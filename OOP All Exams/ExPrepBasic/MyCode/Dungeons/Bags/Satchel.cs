using System;
using System.Collections.Generic;
using System.Text;
using DungeonsAndCodeWizards.Items;

namespace DungeonsAndCodeWizards.Bags
{
    public class Satchel : Bag
    {
        private const int SatchelCapacity = 20;

        public Satchel() 
            : base(SatchelCapacity)
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
