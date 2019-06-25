using StorageMaster.Factories;
using StorageMaster.Products;
using StorageMaster.Storages;
using StorageMaster.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Core
{
    public class StorageMaster
    {
        private ProductFactory ProductFactory;
        private Dictionary<string, Stack<Product>> Pool;
        private Dictionary<string, Storage> StorageRegistry;
        private StorageFactory StorageFactory;
        private Vehicle CurrentVehicle;

        public StorageMaster()
        {
            this.StorageRegistry = new Dictionary<string, Storage>();
            this.Pool = new Dictionary<string, Stack<Product>>();
            this.ProductFactory = new ProductFactory();
            this.StorageFactory = new StorageFactory();
        }

        public string AddProduct(string type, double price)
        {
            Product product = this.ProductFactory.CreateProduct(type, price);
            if (!this.Pool.ContainsKey(type))
            {
                Pool.Add(type, new Stack<Product>());
            }

            Pool[type].Push(product);

            string result = $"Added {type} to pool";
            return result;
        }

        public string RegisterStorage(string type, string name)
        {
            Storage storage = this.StorageFactory.CreateStorage(type, name);
            this.StorageRegistry.Add(name, storage);
            string result = $"Registered {name}";
            return result;
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            CurrentVehicle = StorageRegistry[storageName].GetVehicle(garageSlot);

            string type =  CurrentVehicle.GetType().Name;

            string result = $"Selected {type}";

            return result;
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int loadedProductsCount = 0;
            int totalProducts = productNames.Count();

            foreach (var product in productNames)
            {
                if (CurrentVehicle.IsFull)
                {
                    break;
                }

                if (!this.Pool.ContainsKey(product) || !this.Pool[product].Any())
                {
                    throw new InvalidOperationException($"{product} is out of stock!");
                }
                Product toLoad = Pool[product].Pop();
                this.CurrentVehicle.LoadProduct(toLoad);
                loadedProductsCount++;
            }

            string result = $"Loaded {loadedProductsCount}/{totalProducts} products into {this.CurrentVehicle.GetType().Name}";
            return result;
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            if (!StorageRegistry.ContainsKey(sourceName))
            {
                throw new InvalidOperationException("Invalid source storage!");
            }
            else if (!StorageRegistry.ContainsKey(destinationName))
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }
            Vehicle vehicle = StorageRegistry[sourceName].GetVehicle(sourceGarageSlot);
            string vehicleType = vehicle.GetType().Name;

            Storage destination = StorageRegistry[destinationName];

            Storage source = StorageRegistry[sourceName];

            int destinationGarageSlot = source.SendVehicleTo(sourceGarageSlot, destination);

            string result = $"Sent {vehicleType} to {destinationName} (slot {destinationGarageSlot})";

            return result;
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            Storage storage = StorageRegistry[storageName];
            Vehicle vehicle = storage.GetVehicle(garageSlot);
            int products = vehicle.Trunk.Count();

            int unloadedProducts = storage.UnloadVehicle(garageSlot);

            string result = $"Unloaded {unloadedProducts}/{products} products at {storageName}";
            return result;
        }

        public string GetStorageStatus(string storageName)
        {
            Storage storage = StorageRegistry[storageName];
            Dictionary<string, int> productsStatus = new Dictionary<string, int>();

            foreach (var product in storage.Products)
            {
                if (!productsStatus.ContainsKey(product.GetType().Name))
                {
                    productsStatus.Add(product.GetType().Name, 1);
                }
                else
                {
                    productsStatus[product.GetType().Name]++;
                }
            }

            double totalProductsWeight = storage.Products.Sum(p => p.Weight);
            int storageCapacity = storage.Capacity;
            Dictionary<string, int> sortedProductsStatus = productsStatus
                .OrderByDescending(p => p.Value)
                .ThenBy(p => p.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            string[] productsStatusString = new string[sortedProductsStatus.Count];
            int index = 0;

            foreach (var pair in sortedProductsStatus)
            {
                string info = $"{pair.Key} ({pair.Value})";
                productsStatusString[index] = info;
                index++;
            }

            string[] garageStatus = new string[storage.GarageSlots];
            int garageIndex = 0;

            foreach (var vehicle in storage.Garage)
            {
                if (vehicle == null)
                {
                    garageStatus[garageIndex] = "empty";
                }
                else
                {
                    garageStatus[garageIndex] = vehicle.GetType().Name;
                }
                garageIndex++;
            }

            string stockFormar = $"Stock ({totalProductsWeight}/{storageCapacity}): [{string.Join(", ", productsStatusString)}]";
            string garageFormat = $"Garage: [{string.Join("|", garageStatus)}]";

            string result = stockFormar + Environment.NewLine + garageFormat;

            return result;
        }

        public string GetSummary()
        {
            Storage[] storages = this.StorageRegistry
                .Select(s => s.Value)
                .OrderByDescending(s => s.Products.Sum(p => p.Price))
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var s in storages)
            {
                sb.AppendLine($"{s.Name}:")
                    .AppendLine($"Storage worth: ${s.Products.Sum(p => p.Price):F2}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

    }
}
