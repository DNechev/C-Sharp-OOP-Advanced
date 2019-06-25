using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Entities.Airplanes
{
    public class MediumAirplane : Airplane
    {
        private const int baggageCompartments = 14;
        private const int seats = 10;

        public MediumAirplane()
            : base(seats, baggageCompartments)
        {
        }
    }
}
