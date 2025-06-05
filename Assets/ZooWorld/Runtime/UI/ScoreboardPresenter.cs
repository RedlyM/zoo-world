using ZooWorld.Runtime.Core;
using UniRx;

namespace ZooWorld.Runtime.UI
{
    public class ScoreboardPresenter
    {
        private readonly ScoreboardView _view;
        private readonly IGameStats _stats;

        public ScoreboardPresenter(ScoreboardView view, IGameStats stats)
        {
            _view = view;
            _stats = stats;
        }

        public void Init()
        {
            _stats.PredatorDeaths
                .Subscribe(count =>
                {
                    _view.DeadPredators.text = count.ToString();
                });

            _stats.PreyDeaths
                .Subscribe(count =>
                {
                    _view.DeadPreys.text = count.ToString();
                });
        }
    }
}