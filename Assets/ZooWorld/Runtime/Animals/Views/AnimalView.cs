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

        [SerializeField]
        private float _animationsDuration = 0.5f;

        private void OnCollisionEnter(Collision other)
        {
            OnCollision?.Invoke(other);
        }

        public async UniTask PlayAppearAnimationAsync(CancellationToken token)
        {
            await transform.DOScale(Vector3.one, _animationsDuration)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();
        }

        public async UniTask PlayDieAnimationAsync(CancellationToken token)
        {
            await transform.DOScale(Vector3.zero, _animationsDuration)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();
        }
    }
}