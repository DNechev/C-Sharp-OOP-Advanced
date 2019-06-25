using SoftUniRestaurant.Models.Drinks.Contracts;
using SoftUniRestaurant.Models.Foods.Contracts;
using SoftUniRestaurant.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SoftUniRestaurant.Models.Tables
{
    public abstract class Table : ITable
    {
        private readonly List<IFood> foodOrders;
        private readonly List<IDrink> drinkOrders;
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserved;
        private decimal price;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
            this.foodOrders = new List<IFood>();
            this.drinkOrders = new List<IDrink>();
        }

        public decimal Price
        {
            get { return this.PricePerPerson * this.NumberOfPeople; }
        }

        public bool IsReserved
        {
            get { return isReserved; }
            set { isReserved = value; }
        }

        public decimal PricePerPerson
        {
            get { return pricePerPerson; }
            private set { pricePerPerson = value; }
        }

        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Cannot place zero or less people!");
                }
                numberOfPeople = value;
            }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity has to be greater than 0");
                }
                capacity = value;
            }
        }

        public int TableNumber
        {
            get { return tableNumber; }
            private set { tableNumber = value; }
        }

        public List<IFood> FoodOrders => foodOrders;

        public List<IDrink> DrinkOrders => drinkOrders;

        public void Reserve(int numberOfPeople)
        {
            this.NumberOfPeople = numberOfPeople;
            this.IsReserved = true;
        }

        public void OrderFood(IFood food)
        {
            this.foodOrders.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        public decimal GetBill()
        {
            decimal bill = 0;
            bill = this.DrinkOrders.Sum(d => d.Price) + this.FoodOrders.Sum(f => f.Price) + this.Price;
            return bill;
        }

        public void Clear()
        {
            this.foodOrders.Clear();
            this.drinkOrders.Clear();
            this.IsReserved = false;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder freeTableInfo = new StringBuilder();

            freeTableInfo.AppendLine($"Table: {this.TableNumber}")
                .AppendLine($"Type: {this.GetType().Name}")
                .AppendLine($"Capacity: {this.Capacity}")
                .AppendLine($"Price per Person: {this.PricePerPerson}");

            string result = freeTableInfo.ToString().TrimEnd();

            return result;
        }

        public string GetOccupiedTableInfo()
        {
            StringBuilder occupiedTableInfo = new StringBuilder();

            occupiedTableInfo.AppendLine($"Table: {this.TableNumber}")
              .AppendLine($"Type: {this.GetType().Name}")
              .AppendLine($"Number of people: {this.NumberOfPeople}");

            if (!this.foodOrders.Any())
            {
                occupiedTableInfo.AppendLine("Food orders: None");
            }
            else
            {
                occupiedTableInfo.AppendLine($"Food orders: {this.foodOrders.Count}");

                foreach (var food in this.foodOrders)
                {
                    occupiedTableInfo.AppendLine(food.ToString());
                }
            }

            if (!this.drinkOrders.Any())
            {
                occupiedTableInfo.AppendLine("Drink orders: None");
            }
            else
            {
                occupiedTableInfo.AppendLine($"Drink orders: {this.drinkOrders.Count}");

                foreach (var drink in this.drinkOrders)
                {
                    occupiedTableInfo.AppendLine(drink.ToString());
                }
            }
            string result = occupiedTableInfo.ToString().TrimEnd();
            return result;
        }
    }
}
