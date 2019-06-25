using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Vehicles;

namespace StorageMaster.Storages
{
    public class Warehouse : Storage
    {
        private const int capacity = 10;
        private const int slots = 10;

        public Warehouse(string name) 
            : base(name, capacity, slots, new[] { new Semi(), new Semi(), new Semi()})
        {
        }
    }
}
