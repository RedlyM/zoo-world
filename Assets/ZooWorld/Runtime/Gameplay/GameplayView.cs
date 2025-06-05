using UnityEngine;

namespace ZooWorld.Runtime.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        public Transform Parent => _parent;

        public Transform[] SpawnPoints => _spawnPoints;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private Transform[] _spawnPoints;
    }
}