using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Entities.Airplanes
{
    public class LightAirplane : Airplane
    {
        private const int seats = 5;
        private const int baggageCompartments = 8;

        public LightAirplane() 
            : base(seats, baggageCompartments)
        {
        }
    }
}
