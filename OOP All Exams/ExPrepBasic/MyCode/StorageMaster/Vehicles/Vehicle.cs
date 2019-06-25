using StorageMaster.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StorageMaster.Vehicles
{
    public abstract class Vehicle
    {
        private int capacity;
        private bool isFull;
        private bool isEmpty;
        private List<Product> trunk;

        public Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.trunk = new List<Product>();
        }

        public IReadOnlyCollection<Product> Trunk => this.trunk.AsReadOnly();

        public bool IsEmpty
        {
            get { return isEmpty; }
            protected set
            {
                if (this.Trunk.Count() == 0)
                {
                    isEmpty = true;
                }
                isEmpty = false;
            }
        }


        public bool IsFull
        {
            get { return isFull; }
            protected set
            {
                if (this.Trunk.Sum(x => x.Weight) >= this.Capacity)
                {
                    isFull = true;
                }
                else
                {
                    isFull = false;
                }
            }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }


        public void LoadProduct(Product product)
        {
            if (this.IsFull == true)
            {
                throw new ArgumentException("Vehicle is full!");
            }
            else
            {
                this.trunk.Add(product);
            }
        }

        public Product Unload()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }
            else
            {
                Product product = this.Trunk.Last();
                this.trunk.RemoveAt(this.trunk.Count - 1);
                return product;
            }
        }
    }
}
