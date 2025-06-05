using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ZooWorld.Runtime.Animals
{
    public class AnimalView : MonoBehaviour
    {
        public event Action<Collision> OnCollision;

        public Rigidbody Self => _self;

        public Collider Collider => _collider;

        [SerializeField]
        private Rigidbody _self;

        [SerializeField]
        private Collider _collider;

        private void OnCollisionEnter(Collision other)
        {
            OnCollision?.Invoke(other);
        }

        public async UniTask PlayDieAnimationAsync(CancellationToken token)
        {
            await transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();
        }
    }
}