using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using ZooWorld.Runtime.Core;
using ZooWorld.Runtime.Movements;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalPresenter : IAnimalPresenter, IDisposable
    {
        private readonly AnimalModel _model;
        private readonly AnimalView _view;
        private readonly IMovement _movement;

        [Inject]
        private readonly CollisionHandler _collisionHandler;

        private CancellationTokenSource _moveCts;

        public AnimalPresenter(AnimalModel model, AnimalView view, IMovement movement)
        {
            _model = model;
            _view = view;
            _movement = movement;
        }

        public void Dispose()
        {
            _moveCts?.Cancel();
            _moveCts?.Dispose();
        }

        public void BeginSimulation()
        {
            _moveCts = new CancellationTokenSource();

            _view.OnCollision += HandleCollision;
            _movement.MoveAsync(_view.Self, _moveCts.Token).Forget();
        }

        private void HandleCollision(Collision other)
        {
            if (other.gameObject.TryGetComponent(out AnimalView otherView))
            {
                var collisionResult = _collisionHandler.GetCollisionResult(_model, otherView);

                switch (collisionResult)
                {
                    case CollisionResult.Nothing:
                        Debug.Log("Nothing");
                        break;
                    case CollisionResult.Die:
                        Debug.Log("Die");
                        break;
                    case CollisionResult.Kill:
                        Debug.Log("Kill");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}