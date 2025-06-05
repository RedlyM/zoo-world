using UnityEngine;
using ZooWorld.Runtime.Movements;

namespace ZooWorld.Runtime.Animals.Settings
{
    [System.Serializable]
    public class AnimalData
    {
        public AnimalView Animal => _animal;

        public int Strength => _strength;

        public IMovement MoveStrategy => _moveStrategy;

        [SerializeField]
        private AnimalView _animal;

        [SerializeField]
        private int _strength;

        [SerializeReference]
        private IMovement _moveStrategy;
    }
}