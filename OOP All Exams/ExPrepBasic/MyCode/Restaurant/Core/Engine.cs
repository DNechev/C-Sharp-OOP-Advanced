using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniRestaurant.Core
{
    public class Engine
    {
        private RestaurantController Controller;

        public Engine()
        {
            this.Controller = new RestaurantController();
        }

        public void Run()
        {
            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] commands = input.Split().ToArray();
                string result = string.Empty;

                try
                {
                    if (commands[0] == "AddFood")
                    {
                        string type = commands[1];
                        string name = commands[2];
                        decimal price = decimal.Parse(commands[3]);

                        result = this.Controller.AddFood(type, name, price);
                    }
                    else if (commands[0] == "AddDrink")
                    {
                        string type = commands[1];
                        string name = commands[2];
                        int servingSize = int.Parse(commands[3]);
                        string brand = commands[4];

                        result = this.Controller.AddDrink(type, name, servingSize, brand);
                    }
                    else if (commands[0] == "AddTable")
                    {
                        string type = commands[1];
                        int tableNumber = int.Parse(commands[2]);
                        int capacity = int.Parse(commands[3]);

                        result = this.Controller.AddTable(type, tableNumber, capacity);
                    }
                    else if (commands[0] == "ReserveTable")
                    {
                        int numberOfPeople = int.Parse(commands[1]);

                        result = this.Controller.ReserveTable(numberOfPeople);
                    }
                    else if (commands[0] == "OrderFood")
                    {
                        int tableNumber = int.Parse(commands[1]);
                        string foodName = commands[2];

                        result = this.Controller.OrderFood(tableNumber, foodName);
                    }
                    else if (commands[0] == "OrderDrink")
                    {
                        int tableNumber = int.Parse(commands[1]);
                        string drinkName = commands[2];
                        string drinkBrand = commands[3];

                        result = this.Controller.OrderDrink(tableNumber, drinkName, drinkBrand);
                    }
                    else if (commands[0] == "LeaveTable")
                    {
                        int tableNumber = int.Parse(commands[1]);

                        result = this.Controller.LeaveTable(tableNumber);
                    }
                    else if (commands[0] == "GetFreeTablesInfo")
                    {
                        result = this.Controller.GetFreeTablesInfo();
                    }
                    else if (commands[0] == "GetOccupiedTablesInfo")
                    {
                        result = this.Controller.GetOccupiedTablesInfo();
                    }

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                input = Console.ReadLine();
            }

            string summary = this.Controller.GetSummary();
            Console.WriteLine(summary);
        }
    }
}
