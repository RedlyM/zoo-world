using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Runtime.Animals.Settings;
using ZooWorld.Runtime.Gameplay;
using ZooWorld.Runtime.Spawning;
using ZooWorld.Runtime.UI;

namespace ZooWorld.Runtime.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private AnimalsSettings _animalsSettings;
        [SerializeField] private SpawnSettings _spawnSettings;
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private TMP_Text _tastyMessage;
        [SerializeField] private Transform _tastyMessageParent;
        [SerializeField] private ScoreboardView _scoreboardView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStats>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.Register<ScoreboardPresenter>(Lifetime.Scoped).WithParameter(_scoreboardView);
            builder.Register<CollisionHandler>(Lifetime.Scoped);
            builder.Register<AnimalEmotions>(Lifetime.Scoped).WithParameter(_tastyMessage).WithParameter(_tastyMessageParent);
            builder.Register<GameplayModel>(Lifetime.Scoped);
            builder.Register<GameplayPresenter>(Lifetime.Scoped)
                .WithParameter(_gameplayView)
                .WithParameter(_spawnSettings)
                .WithParameter(_animalsSettings)
                .WithParameter(_mainCamera);

            builder.RegisterEntryPoint<EntryPoint>();
        }
    }
}