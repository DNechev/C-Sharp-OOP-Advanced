using System;
using System.Collections.Generic;
using System.Text;
using DungeonsAndCodeWizards.Items;

namespace DungeonsAndCodeWizards.Bags
{
    public class Backpack : Bag
    {
        private const int BackpackCapacity = 100;

        public Backpack() 
            : base(BackpackCapacity)
        {
        }

        public override void AddItem(Item item)
        {
            base.AddItem(item);
        }

        public override Item GetItem(string name)
        {
            return base.GetItem(name);
        }
    }
}
