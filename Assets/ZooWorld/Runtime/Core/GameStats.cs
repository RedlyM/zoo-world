using UniRx;

namespace ZooWorld.Runtime.Core
{
    public class GameStats : IGameStats
    {
        public IReadOnlyReactiveProperty<int> PredatorDeaths => _predatorDeaths;
        public IReadOnlyReactiveProperty<int> PreyDeaths => _preyDeaths;

        private readonly ReactiveProperty<int> _predatorDeaths = new();
        private readonly ReactiveProperty<int> _preyDeaths = new();

        public void IncreasePredatorDeaths()
        {
            _predatorDeaths.Value += 1;
        }

        public void IncreasePreyDeaths()
        {
            _preyDeaths.Value += 1;
        }
    }
}