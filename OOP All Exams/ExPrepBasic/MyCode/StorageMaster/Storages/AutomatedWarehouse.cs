using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Vehicles;

namespace StorageMaster.Storages
{
    public class AutomatedWarehouse : Storage
    {
        private const int capacity = 1;
        private const int slots = 2;
        

        public AutomatedWarehouse(string name) 
            : base(name, capacity, slots, new[] {new Truck()} )
        {
        }
    }
}
