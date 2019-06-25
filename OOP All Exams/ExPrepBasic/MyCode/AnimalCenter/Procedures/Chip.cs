using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Procedures
{
    public class Chip : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            if (animal.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("ArgumentException: Animal doesn't have enough procedure time");
            }
            else
            {
                if (animal.IsChipped == true)
                {
                    throw new ArgumentException($"ArgumentException: {animal.Name} is already chipped");
                }
                else if (animal.IsChipped == false)
                {
                    animal.Happiness = animal.Happiness - 5;
                    animal.ProcedureTime -= procedureTime;
                    animal.IsChipped = true;
                }
            }
        }
    }
}
