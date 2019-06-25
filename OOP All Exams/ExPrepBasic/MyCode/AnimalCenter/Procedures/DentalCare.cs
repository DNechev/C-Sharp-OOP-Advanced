﻿using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Procedures
{
    public class DentalCare : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            if (animal.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("ArgumentException: Animal doesn't have enough procedure time");
            }
            else
            {
                animal.Happiness += 12;
                animal.Energy += 10;
                animal.ProcedureTime -= procedureTime;
            }
        }
    }
}
