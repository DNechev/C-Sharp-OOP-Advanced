using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Products
{
    public class HardDrive : Product
    {
        private const double hdWeight = 1;

        public HardDrive(double price) : base(price, hdWeight)
        {
        }
    }
}
