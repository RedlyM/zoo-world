using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZooWorld.Runtime.Movements
{
    [System.Serializable]
    public class JumpMovement : IMovement
    {
        [SerializeField]
        private float _power;

        [SerializeField]
        private Vector3[] _directions;

        public async UniTask MoveAsync(Rigidbody target, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var randomDelay = Random.Range(1500, 4000);
                var direction = _directions[Random.Range(0, _directions.Length)];

                await UniTask.Delay(randomDelay, cancellationToken: token);

                var jumpDirection = direction * _power + Vector3.up * _power;
                target.AddForce(jumpDirection, ForceMode.Impulse);
            }
        }
    }
}