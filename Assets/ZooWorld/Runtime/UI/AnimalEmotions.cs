using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using ZooWorld.Runtime.Utils;

namespace ZooWorld.Runtime.UI
{
    public class AnimalEmotions
    {
        private readonly Pool<TMP_Text> _tastyPool;

        public AnimalEmotions(TMP_Text tastyMessage, Transform parent)
        {
            _tastyPool = new Pool<TMP_Text>(tastyMessage, parent);
        }

        public void ShowTasty(Transform target)
        {
            var message = _tastyPool.Get();

            var follow = Observable.EveryLateUpdate()
                .Subscribe(_ => message.transform.position = target.position);

            UniTask.Void(async () =>
            {
                await UniTask.Delay(2000);
                follow.Dispose();
                _tastyPool.Return(message);
            });
        }
    }
}