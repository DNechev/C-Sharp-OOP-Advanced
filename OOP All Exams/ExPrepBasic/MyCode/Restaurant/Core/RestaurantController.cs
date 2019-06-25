namespace SoftUniRestaurant.Core
{
    using SoftUniRestaurant.Models;
    using SoftUniRestaurant.Models.Drinks;
    using SoftUniRestaurant.Models.Drinks.Contracts;
    using SoftUniRestaurant.Models.Foods;
    using SoftUniRestaurant.Models.Foods.Contracts;
    using SoftUniRestaurant.Models.Tables;
    using SoftUniRestaurant.Models.Tables.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RestaurantController
    {
        private List<IFood> menu;
        private List<IDrink> drinks;
        private List<Table> tables;
        private FoodFactory foodFactory;
        private DrinkFactory drinkFactory;
        private TableFactory tableFactory;
        private decimal bills = 0;

        public RestaurantController()
        {
            this.menu = new List<IFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<Table>();
            this.foodFactory = new FoodFactory();
            this.drinkFactory = new DrinkFactory();
            this.tableFactory = new TableFactory();
        }

        public string AddFood(string type, string name, decimal price)
        {
            var food = foodFactory.CreateFood(type, name, price);
            string result = string.Empty;

            if (food != null)
            {
                this.menu.Add(food);
                result = $"Added {food.Name} ({food.GetType().Name}) with price {food.Price:f2} to the pool";
            }

            return result;
        }

        public string AddDrink(string type, string name, int servingSize, string brand)
        {
            var drink = drinkFactory.CreateDrink(type, name, servingSize, brand);
            string result = string.Empty;

            if (drink != null)
            {
                this.drinks.Add(drink);
                result = $"Added {drink.Name} ({drink.Brand}) to the drink pool";
            }

            return result;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            var table = tableFactory.CreateTable(type, tableNumber, capacity);
            string result = string.Empty;

            if (table != null)
            {
                this.tables.Add(table);
                result = $"Added table number {table.TableNumber} in the restaurant";
            }
            return result;
            //could throw an empty line
        }

        public string ReserveTable(int numberOfPeople)
        {
            string result = string.Empty;
            var table = tables.FirstOrDefault(t => t.IsReserved == false && t.Capacity >= numberOfPeople);
            if (table == null)
            {
                result = $"No available table for {numberOfPeople} people";
            }
            else
            {
                table.IsReserved = true;
                table.NumberOfPeople = numberOfPeople;
                result = $"Table {table.TableNumber} has been reserved for {numberOfPeople} people";
            }
            return result;
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return $"Could not find table with {tableNumber}";
            }
            var food = menu.FirstOrDefault(f => f.Name == foodName);
            if (food == null)
            {
                return $"No {foodName} in the menu";
            }

            table.OrderFood(food);
            return $"Table {tableNumber} ordered {foodName}";
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return $"Could not find table with {tableNumber}";
            }
            var drink = drinks.FirstOrDefault(f => f.Name == drinkName && f.Brand == drinkBrand);
            if (drink == null)
            {
                return $"There is no {drinkName} {drinkBrand} available";
            }

            table.OrderDrink(drink);
            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string LeaveTable(int tableNumber)
        {

            decimal bill = 0m;

            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            bill = table.GetBill();
            bills += bill;
            table.Clear();

            string tableString = $"Table: {tableNumber}";
            string billString = $"Bill: {bill:f2}";

            string[] output = new string[] { tableString, billString };

            string result = string.Join(Environment.NewLine, output);

            return result;
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var table in tables.Where(t => t.IsReserved == false))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }
            string result = sb.ToString().Trim();
            return result;
        }

        public string GetOccupiedTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var table in tables.Where(t => t.IsReserved == true))
            {
                sb.AppendLine(table.GetOccupiedTableInfo());
            }
            string result = sb.ToString().Trim();
            return result;
        }

        public string GetSummary()
        {
            string result = $"Total income: {bills:f2}lv";
            return result;
        }
    }
}
