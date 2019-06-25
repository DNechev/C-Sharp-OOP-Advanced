using DungeonsAndCodeWizards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards.Bags
{
    public abstract class Bag
    {
        private int capacity;
        private List<Item> items;
        private int load;

        public Bag(int capacity)
        {
            this.Capacity = capacity;
            this.items = new List<Item>();
        }

        public int Load
        {
            get { return load; }
            private set
            {
                load = this.Items.Sum(i => i.Weight);
            }
        }

        public IReadOnlyCollection<Item> Items => this.items.AsReadOnly();

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                capacity = 100;
            }
        }

        public virtual void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.Capacity)
            {
                throw new InvalidOperationException("Bag is full!");
            }
            this.items.Add(item);
        }

        public virtual Item GetItem(string name)
        {
            if (!this.Items.Any())
            {
                throw new InvalidOperationException("Bag is empty!");
            }

            Item item = items.FirstOrDefault(i => i.GetType().Name == name);

            if (item == null)
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }
            return item;
        }
    }
}
