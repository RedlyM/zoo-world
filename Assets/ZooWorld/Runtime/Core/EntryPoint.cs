using VContainer.Unity;
using ZooWorld.Runtime.Gameplay;
using ZooWorld.Runtime.UI;

namespace ZooWorld.Runtime.Core
{
    public class EntryPoint : IInitializable
    {
        private readonly GameplayPresenter _gameplayPresenter;
        private readonly ScoreboardPresenter _scoreboardPresenter;

        public EntryPoint(GameplayPresenter gameplayPresenter, ScoreboardPresenter scoreboardPresenter)
        {
            _gameplayPresenter = gameplayPresenter;
            _scoreboardPresenter = scoreboardPresenter;
        }

        void IInitializable.Initialize()
        {
            _gameplayPresenter.StartGameplay();
            _scoreboardPresenter.Init();
        }
    }
}