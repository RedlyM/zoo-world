using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using ZooWorld.Runtime.Animals;
using ZooWorld.Runtime.Animals.Settings;
using ZooWorld.Runtime.Spawning;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ZooWorld.Runtime.Gameplay
{
    public class GameplayPresenter : IDisposable
    {
        private readonly GameplayModel _gameplayModel;
        private readonly GameplayView _gameplayView;
        private readonly AnimalsSettings _animalsSettings;
        private readonly SpawnSettings _settings;
        private readonly Camera _mainCamera;
        private readonly IObjectResolver _resolver;

        private Rect _bounds;
        private CancellationTokenSource _cts;

        public GameplayPresenter(GameplayModel gameplayModel, GameplayView gameplayView,
            AnimalsSettings animalsSettings, SpawnSettings settings, Camera mainCamera, IObjectResolver resolver)
        {
            _gameplayModel = gameplayModel;
            _gameplayView = gameplayView;
            _animalsSettings = animalsSettings;
            _settings = settings;
            _mainCamera = mainCamera;
            _resolver = resolver;
        }

        public void StartGameplay()
        {
            _cts = new CancellationTokenSource();
            _bounds = CalculateWorldBounds(_mainCamera);
            BeginSpawnAsync(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTaskVoid BeginSpawnAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_settings.SpawnDelay, cancellationToken: token);
                var randomAnimal = GetRandomAnimal();

                if (!_gameplayModel.TryGetFreeAnimal(randomAnimal.Animal.name, out var presenter, out var model, out var view))
                {
                    view = Object.Instantiate(randomAnimal.Animal, _gameplayView.Parent);
                    model = new AnimalModel(randomAnimal.Strength, randomAnimal.CanEatSameStrength, randomAnimal.Animal.name);
                    presenter = new AnimalPresenter(model, view, randomAnimal.MoveStrategy);

                    _resolver.Inject(presenter);
                    _gameplayModel.AddAnimal(presenter, model, view);
                }

                var position = GetRandomSpawnPoint();
                view.transform.position = position;
                view.Self.position = position;
                presenter.BeginSimulation();
            }
        }

        private AnimalData GetRandomAnimal()
        {
            return _animalsSettings.Animals[Random.Range(0, _animalsSettings.Animals.Length)];
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var x = Random.Range(_bounds.xMin, _bounds.xMax);
            var z = Random.Range(_bounds.yMin, _bounds.yMax);
            return new Vector3(x, _gameplayView.Parent.position.y, z);
        }

        private Rect CalculateWorldBounds(Camera camera)
        {
            var halfHeight = camera.orthographicSize;
            var halfWidth = camera.aspect * halfHeight;

            var camPos = camera.transform.position;

            var xMin = camPos.x - halfWidth;
            var zMin = camPos.z - halfHeight;
            var width = halfWidth * 2f;
            var height = halfHeight * 2f;

            return new Rect(xMin, zMin, width, height);
        }
    }
}