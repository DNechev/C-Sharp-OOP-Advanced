using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Products
{
    public class SolidStateDrive : Product
    {
        private const double ssdWeight = 0.2;

        public SolidStateDrive(double price) : base(price, ssdWeight)
        {
        }
    }
}
