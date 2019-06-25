﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Products
{
    public class Ram : Product
    {
        private const double ramWeight = 0.1;

        public Ram(double price) : base(price, ramWeight)
        {
        }
    }
}
