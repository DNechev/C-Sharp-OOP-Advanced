using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Vehicles;

namespace StorageMaster.Storages
{
    public class DistributionCenter : Storage
    {
        private const int capacity = 2;
        private const int slots = 5;

        public DistributionCenter(string name) 
            : base(name, capacity, slots, new[] { new Van(), new Van(), new Van() } )
        {
        }
    }
}
