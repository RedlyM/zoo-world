using System;
using UniRx;
using UnityEngine;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalModel
    {
        public readonly int Strength;
        public readonly bool CanEatSameStrength;
        public readonly string Identifier;

        public bool IsAlive { get; private set; }
        public float LifetimeSeconds { get; private set; }

        private IDisposable _lifetime;

        public AnimalModel(int strength, bool canEatSameStrength, string identifier)
        {
            Strength = strength;
            CanEatSameStrength = canEatSameStrength;
            Identifier = identifier;
        }

        public void Live()
        {
            LifetimeSeconds = 0f;
            IsAlive = true;

            _lifetime = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    LifetimeSeconds += Time.deltaTime;
                });
        }

        public void Kill()
        {
            LifetimeSeconds = 0f;
            IsAlive = false;

            _lifetime?.Dispose();
        }
    }
}