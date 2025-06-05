using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZooWorld.Runtime.Movements
{
    [System.Serializable]
    public class LinearMovement : IMovement
    {
        [SerializeField]
        private float _directionChangeTimeMin;

        [SerializeField]
        private float _directionChangeTimeMax;

        [SerializeField]
        private Vector3[] _directions;

        public UniTask MoveAsync(Rigidbody target, CancellationToken token)
        {
            var nextChangeTime = 0f;
            var direction = Vector3.forward;

            var disposable = Observable
                .EveryFixedUpdate()
                .Subscribe(l =>
                {
                    if (Time.time > nextChangeTime)
                    {
                        direction = _directions[Random.Range(0, _directions.Length)];
                        nextChangeTime += Random.Range(_directionChangeTimeMin, _directionChangeTimeMax);
                    }

                    token.ThrowIfCancellationRequested();

                    target.velocity = new Vector3(direction.x, target.velocity.y, direction.z);
                });

            token.Register(() => disposable?.Dispose());

            return UniTask.CompletedTask;
        }

        public UniTask MoveBackAsync(Rigidbody target, CancellationToken token)
        {
            var direction = -target.velocity.normalized;
            return RotateAsync(target.transform, direction, 0.5f, token);
        }

        private async UniTask RotateAsync(Transform transform, Vector3 newDirection, float duration, CancellationToken token)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
            await transform.DORotateQuaternion(targetRotation, duration).AsyncWaitForCompletion();
        }
    }
}