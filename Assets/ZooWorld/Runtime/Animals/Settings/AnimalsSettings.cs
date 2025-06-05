using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.Runtime.Animals.Settings
{
    [CreateAssetMenu(fileName = "AnimalsSettings", menuName = "ZooWorld/AnimalsSettings", order = 0)]
    public class AnimalsSettings : SerializedScriptableObject
    {
        public AnimalData[] Animals => _animals;

        [SerializeReference] private AnimalData[] _animals;
    }
}