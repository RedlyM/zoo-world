using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZooWorld.Runtime.Movements
{
    [System.Serializable]
    public class LinearMovement : IMovement
    {
        [SerializeField]
        private Vector3[] _directions;

        public async UniTask MoveAsync(Rigidbody target, CancellationToken token)
        {
            var nextChangeTime = DateTime.UtcNow;
            var direction = _directions[Random.Range(0, _directions.Length)];

            while (!token.IsCancellationRequested)
            {
                if (DateTime.UtcNow > nextChangeTime)
                {
                    direction = _directions[Random.Range(0, _directions.Length)];
                    nextChangeTime = DateTime.UtcNow.AddSeconds(2);
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);

                target.velocity = new Vector3(direction.x, target.velocity.y, direction.z);
            }
        }
    }
}