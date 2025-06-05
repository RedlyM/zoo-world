using TMPro;
using UnityEngine;

namespace ZooWorld.Runtime.UI
{
    public class ScoreboardView : MonoBehaviour
    {
        public TMP_Text DeadPredators => _deadPredators;

        public TMP_Text DeadPreys => _deadPreys;

        [SerializeField]
        private TMP_Text _deadPredators;

        [SerializeField]
        private TMP_Text _deadPreys;

    }
}