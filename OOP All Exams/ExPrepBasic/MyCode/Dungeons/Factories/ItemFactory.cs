using DungeonsAndCodeWizards.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.Factories
{
    public class ItemFactory
    {
        public Item createItem(string name)
        {
            if (name == "HealthPotion")
            {
                HealthPotion healthPotion = new HealthPotion();
                return healthPotion;
            }
            else if (name == "PoisonPotion")
            {
                PoisonPotion poisonPotion = new PoisonPotion();
                return poisonPotion;
            }
            else if (name == "ArmorRepairKit")
            {
                ArmorRepairKit armorRepairKit = new ArmorRepairKit();
                return armorRepairKit;
            }
            else
            {
                throw new ArgumentException($"Invalid item \"{name}\"!");
            }
        }
    }
}
