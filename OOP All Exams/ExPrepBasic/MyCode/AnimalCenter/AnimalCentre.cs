using AnimalCentre.Procedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre
{
    public class AnimalCentre
    {
        private Hotel hotel;
        private Chip chiping;
        private Vaccinate vaccine;
        private Fitness fitnessPr;
        private Play playing;
        private DentalCare dental;
        private NailTrim nailJob;

        public AnimalCentre()
        {
            this.Hotel = new Hotel();
            this.Chiping = new Chip();
            this.Vaccine = new Vaccinate();
            this.FitnessPr = new Fitness();
            this.Playing = new Play();
            this.Dental = new DentalCare();
            this.NailJob = new NailTrim();
        }

        public NailTrim NailJob
        {
            get { return nailJob; }
            set { nailJob = value; }
        }


        public DentalCare Dental
        {
            get { return dental; }
            set { dental = value; }
        }


        public Play Playing
        {
            get { return playing; }
            set { playing = value; }
        }


        public Fitness FitnessPr
        {
            get { return fitnessPr; }
            set { fitnessPr = value; }
        }


        public Vaccinate Vaccine
        {
            get { return vaccine; }
            set { vaccine = value; }
        }


        public Chip Chiping
        {
            get { return chiping; }
            set { chiping = value; }
        }

        public Hotel Hotel
        {
            get { return hotel; }
            set { hotel = value; }
        }

        public string RegisterAnimal(string type, string name, int energy, int happiness, int procedureTime)
        {
            try
            {

                if (type == "Cat")
                {
                    Cat cat = new Cat(name, energy, happiness, procedureTime);
                    Hotel.Accommodate(cat);
                    return $"Animal {name} registered successfully";
                }

                else if (type == "Dog")
                {
                    Dog dog = new Dog(name, energy, happiness, procedureTime);
                    Hotel.Accommodate(dog);
                    return $"Animal {name} registered successfully";
                }

                else if (type == "Lion")
                {
                    Lion lion = new Lion(name, energy, happiness, procedureTime);
                    Hotel.Accommodate(lion);
                    return $"Animal {name} registered successfully";
                }

                else
                {
                    Pig pig = new Pig(name, energy, happiness, procedureTime);
                    Hotel.Accommodate(pig);
                    return $"Animal {name} registered successfully";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Chip(string name, int procedureTime)
        {
            if (this.Hotel.Animals.ContainsKey(name))
            {
                Chiping.DoService(this.Hotel.Animals[name], procedureTime);
                return $"{name} had chip procedure";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string Vaccinate(string name, int procedureTime)
        {
            if (Hotel.Animals.ContainsKey(name))
            {
                Vaccine.DoService(Hotel.Animals[name], procedureTime);
                return $"{name} had vaccination procedure";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string Fitness(string name, int procedureTime)
        {
            if (Hotel.Animals.ContainsKey(name))
            {
                FitnessPr.DoService(Hotel.Animals[name], procedureTime);
                return $"{name} had fitness procedure";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string Play(string name, int procedureTime)
        {
            if (Hotel.Animals.ContainsKey(name))
            {
                Playing.DoService(Hotel.Animals[name], procedureTime);
                return $"{name} was playing for {procedureTime} hours";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string DentalCare(string name, int procedureTime)
        {
            if (Hotel.Animals.ContainsKey(name))
            {
                Dental.DoService(Hotel.Animals[name], procedureTime);
                return $"{name} had dental care procedure";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string NailTrim(string name, int procedureTime)
        {
            if (Hotel.Animals.ContainsKey(name))
            {
                NailJob.DoService(Hotel.Animals[name], procedureTime);
                return $"{name} had nail trim procedure";
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {name} does not exist");
            }
        }

        public string Adopt(string animalName, string owner)
        {
            if (Hotel.Animals.ContainsKey(animalName) && Hotel.Animals[animalName].IsAdopt == false)
            {
                Hotel.Animals[animalName].IsAdopt = true;
                if (Hotel.Animals[animalName].IsChipped == true)
                {
                    return $"{owner} adopted animal with chip";
                }
                else
                {
                    return $"{owner} adopted animal without chip";
                }
            }
            else
            {
                throw new ArgumentException($"ArgumentException: Animal {animalName} does not exist");
            }
        }

        public string History(string type)
        {
            if (type == "Chip")
            {
                return Chiping.History();
            }
            else if (type == "DentalCare")
            {
                return Dental.History();
            }
            else if (type == "Fitness")
            {
                return FitnessPr.History();
            }
            else if (type == "NailTrim")
            {
                return NailJob.History();
            }
            else if (type == "Play")
            {
                return Playing.History();
            }
            else
            {
                return Vaccine.History();
            }
        }

    }
}
