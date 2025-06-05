using UnityEngine;

namespace ZooWorld.Runtime.Spawning
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "ZooWorld/SpawnSettings", order = 0)]
    public class SpawnSettings : ScriptableObject
    {
        public float SpawnDelay => _spawnDelay;

        [SerializeField]
        private float _spawnDelay;
    }
}