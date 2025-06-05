using VContainer.Unity;
using ZooWorld.Runtime.Gameplay;

namespace ZooWorld.Runtime.Core
{
    public class EntryPoint : IInitializable
    {
        private readonly GameplayPresenter _gameplayPresenter;

        public EntryPoint(GameplayPresenter gameplayPresenter)
        {
            _gameplayPresenter = gameplayPresenter;
        }

        void IInitializable.Initialize()
        {
            _gameplayPresenter.StartGameplay();
        }
    }
}