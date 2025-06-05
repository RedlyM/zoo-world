using System.Collections.Generic;
using ZooWorld.Runtime.Animals;

namespace ZooWorld.Runtime.Gameplay
{
    public class GameplayModel
    {
        private readonly Dictionary<AnimalView, AnimalModel> _animals;

        public GameplayModel()
        {
            _animals = new Dictionary<AnimalView, AnimalModel>();
        }

        public void AddAnimal(AnimalView view, AnimalModel model)
        {
            _animals[view] = model;
        }

        public AnimalModel GetAnimalModel(AnimalView view)
        {
            return _animals[view];
        }
    }
}