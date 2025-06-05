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

        public async UniTask PlayAppearAsync(CancellationToken token)
        {
            transform.localScale = Vector3.zero;

            await transform.DOScale(Vector3.one, _animationsDuration)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();
        }

        public async UniTask PlayDieAsync(CancellationToken token)
        {
            _collider.isTrigger = true;

            await transform.DOScale(Vector3.zero, _animationsDuration)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();
        }

        public void Setup()
        {
            _collider.isTrigger = false;
            gameObject.SetActive(true);
        }

        public void ResetState()
        {
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
            _self.velocity = Vector3.zero;
            _self.position = Vector3.zero;
        }
    }
}