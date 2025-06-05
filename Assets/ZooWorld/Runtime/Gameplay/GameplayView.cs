using UnityEngine;

namespace ZooWorld.Runtime.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        public Transform Parent => _parent;

        [SerializeField]
        private Transform _parent;
    }
}