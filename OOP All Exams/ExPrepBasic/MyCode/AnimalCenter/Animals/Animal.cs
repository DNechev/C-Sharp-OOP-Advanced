using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre
{
    public abstract class Animal : IAnimal
    {
        private string name;
        private int happiness;
        private int energy;
        private int procedureTime;
        private string owner;
        private bool isAdopt;
        private bool isChipped;
        private bool isVaccinated;

        public Animal(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.Owner = "Centre";
            this.IsAdopt = false;
            this.IsChipped = false;
            this.IsVaccinated = false;
        }

        public bool IsVaccinated
        {
            get { return isVaccinated; }
            set { isVaccinated = value; }
        }


        public bool IsChipped
        {
            get { return isChipped; }
            set { isChipped = value; }
        }


        public bool IsAdopt
        {
            get { return isAdopt; }
            set { isAdopt = value; }
        }


        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
            }
        }


        public int ProcedureTime
        {
            get { return procedureTime; }
             set { procedureTime = value; }
        }


        public int Energy
        {
            get { return energy; }
             set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("ArgumentException: Invalid energy");
                }
                energy = value;
            }
        }


        public int Happiness
        {
            get { return happiness; }
             set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("ArgumentException: Invalid happiness");
                }
                happiness = value;
            }
        }


        public string Name
        {
            get { return name; }
             set { name = value; }
        }

    }
}
