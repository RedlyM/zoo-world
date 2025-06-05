using System;
using System.Collections.Generic;
using ZooWorld.Runtime.Animals;

namespace ZooWorld.Runtime.Gameplay
{
    public class GameplayModel
    {
        private List<(AnimalPresenter presenter, AnimalModel model, AnimalView view)> _createdAnimals;

        public GameplayModel()
        {
            _createdAnimals = new List<(AnimalPresenter presenter, AnimalModel model, AnimalView view)>(8);
        }

        public void AddAnimal(AnimalPresenter presenter, AnimalModel model, AnimalView view)
        {
            _createdAnimals.Add((presenter, model, view));
        }

        public bool TryGetFreeAnimal(string identifier, out AnimalPresenter presenter, out AnimalModel model, out AnimalView view)
        {
            presenter = null;
            model = null;
            view = null;

            foreach (var createdAnimal in _createdAnimals)
            {
                if (createdAnimal.model.IsAlive || !createdAnimal.model.Identifier.Equals(identifier))
                {
                    continue;
                }

                presenter = createdAnimal.presenter;
                model = createdAnimal.model;
                view = createdAnimal.view;

                return true;
            }

            return false;
        }

        public AnimalModel GetAnimalModel(AnimalView view)
        {
            foreach (var animal in _createdAnimals)
            {
                if (animal.view.Equals(view))
                {
                    return animal.model;
                }
            }

            throw new Exception("Animal model not found");
        }
    }
}