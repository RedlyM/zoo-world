using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Runtime.Animals.Settings;
using ZooWorld.Runtime.Gameplay;
using ZooWorld.Runtime.Spawning;

namespace ZooWorld.Runtime.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AnimalsSettings _animalsSettings;
        [SerializeField] private SpawnSettings _spawnSettings;
        [SerializeField] private GameplayView _gameplayView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CollisionHandler>(Lifetime.Scoped);
            builder.Register<GameplayModel>(Lifetime.Scoped);
            builder.Register<GameplayPresenter>(Lifetime.Scoped)
                .WithParameter(_gameplayView)
                .WithParameter(_spawnSettings)
                .WithParameter(_animalsSettings);

            builder.RegisterEntryPoint<EntryPoint>();
        }
    }
}