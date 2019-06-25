using StorageMaster.Products;
using StorageMaster.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Storages
{
    public abstract class Storage
    {
        private string name;
        private int capacity; //the maximum weight of products the storage can handle
        private int garageSlots;
        private bool isFull;
        private List<Product> products;
        private Vehicle[] garage;

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.products = new List<Product>();
            this.garage = new Vehicle[this.GarageSlots];

            this.ParkInitialVehicles(vehicles);
        }

        private void ParkInitialVehicles(IEnumerable<Vehicle> vehicles)
        {
            int index = 0;
            foreach (var vehicle in vehicles)
            {
                this.garage[index] = vehicle;
                index++;
            }
        }

        public IReadOnlyCollection<Vehicle> Garage => Array.AsReadOnly(garage);
        public IReadOnlyCollection<Product> Products => products.AsReadOnly();

        public bool IsFull => this.products.Sum(x => x.Weight) >= this.Capacity;

        public int GarageSlots
        {
            get { return garageSlots; }
            private set { garageSlots = value; }
        }


        public int Capacity
        {
            get { return capacity; }
            private set { capacity = value; }
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }


        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }

            if (garage[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }

            return this.garage[garageSlot];
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            Vehicle vehicle = GetVehicle(garageSlot);

            int index = deliveryLocation.AddVehicleToGarage(vehicle);
            this.garage[garageSlot] = null;

            return index;
        }

        private int AddVehicleToGarage(Vehicle vehicle)
        {
            int freeGarageSlotIndex = Array.IndexOf(this.garage, null);

           // if (freeGarageSlotIndex == -1)
           // {
           //     throw new InvalidOperationException("No room in garage!");
           // }

            this.garage[freeGarageSlotIndex] = vehicle;
            return freeGarageSlotIndex;
        }

        public int UnloadVehicle(int garageSlot)
        {
            if (this.IsFull == true)
            {
                throw new InvalidOperationException("Storage is full!");
            }

            Vehicle vehicle = GetVehicle(garageSlot);
            int unloadedProducts = 0;

            while (vehicle.IsEmpty == false && this.IsFull == false)
            {
                this.products.Add(vehicle.Unload());
                unloadedProducts++;
            }

            return unloadedProducts;
        }
    }
}
