using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using ZooWorld.Runtime.Core;
using ZooWorld.Runtime.Movements;
using ZooWorld.Runtime.UI;
using ZooWorld.Runtime.Utils;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalPresenter : IDisposable
    {
        private readonly AnimalModel _model;
        private readonly AnimalView _view;
        private readonly IMovement _movement;

        [Inject]
        private readonly CollisionHandler _collisionHandler;

        [Inject]
        private readonly AnimalEmotions _emotions;

        [Inject]
        private readonly GameStats _stats;

        private CancellationTokenSource _lifetimeCts;

        public AnimalPresenter(AnimalModel model, AnimalView view, IMovement movement)
        {
            _model = model;
            _view = view;
            _movement = movement;
        }

        public void Dispose()
        {
            StopSimulation();
        }

        public void BeginSimulation()
        {
            _lifetimeCts = new CancellationTokenSource();

            _view.OnCollision += HandleCollision;
            _model.Live();
            _view.gameObject.SetActive(true);
            _view.Collider.isTrigger = false;
            _view.PlayAppearAnimationAsync(_lifetimeCts.Token).Forget();
            _movement.MoveAsync(_view.Self, _lifetimeCts.Token).Forget();
        }

        private void StopSimulation()
        {
            _model.Kill();

            _view.OnCollision -= HandleCollision;
            _view.gameObject.SetActive(false);
            _view.transform.localPosition = Vector3.zero;
            _view.Self.velocity = Vector3.zero;
            _view.Self.position = Vector3.zero;

            _lifetimeCts?.Cancel();
            _lifetimeCts?.Dispose();
        }

        private void HandleCollision(Collision other)
        {
            if (other.gameObject.TryGetComponent(out AnimalView otherView))
            {
                var collisionResult = _collisionHandler.GetCollisionResult(_model, otherView);

                switch (collisionResult)
                {
                    case CollisionResult.Nothing:
                        _movement.MoveBackAsync(_view.Self, _lifetimeCts.Token).Forget();
                        break;
                    case CollisionResult.Die:

                        if (_model.CanEatSameStrength)
                        {
                            _stats.IncreasePredatorDeaths();
                        }
                        else
                        {
                            _stats.IncreasePreyDeaths();
                        }

                        _view.Collider.isTrigger = true;
                        _view.PlayDieAnimationAsync(_lifetimeCts.Token).ContinueWith(StopSimulation).Forget();
                        break;
                    case CollisionResult.Kill:
                        _emotions.ShowTasty(_view.transform);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if(other.gameObject.TryGetComponent(out Border border))
            {
                _movement.MoveBackAsync(_view.Self, _lifetimeCts.Token);
            }
        }
    }
}