using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace AnimalCentre
{
    public class Hotel
    {
        private const int capacity = 10;
        private readonly Dictionary<string, IAnimal> animals;

        public Hotel()
        {
            this.animals = new Dictionary<string, IAnimal>();
        }

        public IReadOnlyDictionary<string, IAnimal> Animals
        {
            get { return this.animals; }
        }

        public void Accommodate(IAnimal animal)
        {
            if (Animals.Count < 10)
            {
                if (Animals.ContainsKey(animal.Name))
                {
                    throw new ArgumentException($"ArgumentException: Animal {animal.Name} already exist");
                }
                else
                {
                    this.animals.Add(animal.Name, animal);
                }
            }
            else
            {
                throw new InvalidOperationException("InvalidOperationException: Not enough capacity");
            }
        }

        public void Adopt(string animalName, string owner)
        {
            if (!Animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"ArgumentException: Animal {animalName} does not exist");
            }
            else
            {
                Animals[animalName].Owner = owner;
                Animals[animalName].IsAdopt = true;
                this.animals.Remove(animalName);
            }
        }
    }
}
