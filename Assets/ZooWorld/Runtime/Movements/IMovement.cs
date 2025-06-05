using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZooWorld.Runtime.Movements
{
    public interface IMovement
    {
        UniTask MoveAsync(Rigidbody target, CancellationToken token);
        UniTask MoveBackAsync(Rigidbody target, CancellationToken token);
    }
}