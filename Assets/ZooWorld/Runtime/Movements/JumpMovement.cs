using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ZooWorld.Runtime.Movements
{
    [System.Serializable]
    public class JumpMovement : IMovement
    {
        [SerializeField]
        private float _power;

        [SerializeField]
        private float _jumpDelayMin;

        [SerializeField]
        private float _jumpDelayMax;

        [SerializeField]
        private Vector3[] _directions;

        public async UniTask MoveAsync(Rigidbody target, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var randomDelay = Random.Range(_jumpDelayMin, _jumpDelayMax);
                var direction = _directions[Random.Range(0, _directions.Length)];

                await UniTask.WaitForSeconds(randomDelay, cancellationToken: token);

                MakeJump(target, direction);
            }
        }

        public async UniTask MoveBackAsync(Rigidbody target, CancellationToken token)
        {
            var direction = -target.velocity.normalized;
            await RotateAsync(target.transform, direction, 0.56f, token);
            MakeJump(target, direction);
        }

        private void MakeJump(Rigidbody target, Vector3 direction)
        {
            var jumpDirection = direction * _power + Vector3.up * _power;
            target.AddForce(jumpDirection, ForceMode.Impulse);
        }

        private async UniTask RotateAsync(Transform transform, Vector3 newDirection, float duration, CancellationToken token)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
            await transform.DORotateQuaternion(targetRotation, duration).AsyncWaitForCompletion();
        }
    }
}