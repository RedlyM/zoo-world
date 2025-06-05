using ZooWorld.Runtime.Animals;
using ZooWorld.Runtime.Gameplay;

namespace ZooWorld.Runtime.Core
{
    public class CollisionHandler
    {
        private readonly GameplayModel _gameplayModel;

        public CollisionHandler(GameplayModel gameplayModel)
        {
            _gameplayModel = gameplayModel;
        }

        public CollisionResult GetCollisionResult(AnimalModel self, AnimalView other)
        {
            var otherModel = _gameplayModel.GetAnimalModel(other);

            if (self.Strength == otherModel.Strength)
            {
                return CollisionResult.Nothing;
            }

            return self.Strength > otherModel.Strength ? CollisionResult.Kill : CollisionResult.Die;
        }
    }
}