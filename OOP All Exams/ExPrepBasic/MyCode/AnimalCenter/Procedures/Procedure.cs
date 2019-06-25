using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Procedures
{
    public abstract class Procedure : IProcedure
    {

        private ICollection<Animal> procedureHistory;

        public Procedure()
        {
        }

        public ICollection<Animal> ProcedureHistory
        {
            get { return procedureHistory; }
            private set { procedureHistory = value; }
        }

        public virtual string History()
        {
            return "";
        }

        public virtual void DoService(IAnimal animal, int procedureTime)
        {
        }

    }
}
