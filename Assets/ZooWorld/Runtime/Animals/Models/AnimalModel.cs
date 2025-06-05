using System;
using UniRx;
using UnityEngine;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalModel
    {
        public readonly int Strength;
        public readonly bool CanEatSameStrength;

        public IReadOnlyReactiveProperty<float> LifetimeSeconds => _lifetimeSeconds;
        public IReadOnlyReactiveProperty<bool> IsAlive => _isAlive;

        private ReactiveProperty<float> _lifetimeSeconds;
        private ReactiveProperty<bool> _isAlive;

        private IDisposable _lifetime;

        public AnimalModel(int strength, bool canEatSameStrength)
        {
            Strength = strength;
            CanEatSameStrength = canEatSameStrength;
        }

        public void Live()
        {
            _lifetimeSeconds = new ReactiveProperty<float>(0);
            _isAlive = new ReactiveProperty<bool>(true);

            _lifetime = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _lifetimeSeconds.Value += Time.deltaTime;
                });
        }

        public void Kill()
        {
            _lifetime?.Dispose();
            _lifetimeSeconds?.Dispose();
            _isAlive?.Dispose();
        }
    }
}