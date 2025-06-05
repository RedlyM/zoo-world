using UniRx;

namespace ZooWorld.Runtime.Core
{
    public interface IGameStats
    {
        public IReadOnlyReactiveProperty<int> PredatorDeaths { get; }
        public IReadOnlyReactiveProperty<int> PreyDeaths { get; }
    }
}