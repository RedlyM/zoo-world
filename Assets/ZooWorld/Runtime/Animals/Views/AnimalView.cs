using System;
using UnityEngine;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalView : MonoBehaviour
    {
        public event Action<Collision> OnCollision;

        public Rigidbody Self => _self;

        [SerializeField]
        private Rigidbody _self;

        private void OnCollisionEnter(Collision other)
        {
            OnCollision?.Invoke(other);
        }
    }
}