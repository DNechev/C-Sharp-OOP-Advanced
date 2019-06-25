using SoftUniRestaurant.Models.Drinks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Models
{
    public class DrinkFactory
    {
        public Drink CreateDrink(string type, string name, int servingSize, string brand)
        {
            Drink drink = null;

            if (type == "Alcohol")
            {
                drink = new Alcohol(name, servingSize, brand);
            }
            else if (type == "FuzzyDrink")
            {
                drink = new FuzzyDrink(name, servingSize, brand);
            }
            else if (type == "Juice")
            {
                drink = new Juice(name, servingSize, brand);
            }
            else if (type == "Water")
            {
                drink = new Water(name, servingSize, brand);
            }

            return drink;
        }
    }
}
