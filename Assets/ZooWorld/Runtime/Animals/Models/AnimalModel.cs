namespace ZooWorld.Runtime.Animals
{
    public class AnimalModel
    {
        public int Strength { get; private set; }

        public AnimalModel(int strength)
        {
            Strength = strength;
        }
    }
}