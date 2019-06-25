using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Vehicles
{
    public class Van : Vehicle
    {
        private const int vanCapacity = 2;

        public Van() : base(vanCapacity)
        {
        }
    }
}
