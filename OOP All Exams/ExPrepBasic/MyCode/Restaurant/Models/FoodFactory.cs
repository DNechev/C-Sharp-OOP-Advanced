using SoftUniRestaurant.Models.Foods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Models
{
    public class FoodFactory
    {
        public Food CreateFood(string type, string name, decimal price)
        {
            Food food = null;

            if (type == "Dessert")
            {
                food = new Dessert(name, price);
            }
            else if (type == "MainCourse")
            {
                food = new MainCourse(name, price);
            }
            else if (type == "Salad")
            {
                food = new Salad(name, price);
            }
            else if (type == "Soup")
            {
                food = new Soup(name, price);
            }

            return food;
        }
    }
}
