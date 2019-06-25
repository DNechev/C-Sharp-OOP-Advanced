using StorageMaster.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Factories
{
    public class StorageFactory
    {
        public Storage CreateStorage(string type, string name)
        {
            Storage storage = null;

            if (type == "Warehouse")
            {
                storage = new Warehouse(name);
            }
            else if (type == "DistributionCenter")
            {
                storage = new DistributionCenter(name);
            }
            else if (type == "AutomatedWarehouse")
            {
                storage = new AutomatedWarehouse(name);
            }

            if (storage != null)
            {
                return storage;
            }
            else
            {
                throw new InvalidOperationException("Invalid storage type!");
            }
        }
    }
}
