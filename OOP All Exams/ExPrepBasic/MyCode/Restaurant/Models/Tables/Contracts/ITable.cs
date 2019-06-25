using SoftUniRestaurant.Models.Drinks.Contracts;
using SoftUniRestaurant.Models.Foods;
using SoftUniRestaurant.Models.Foods.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SoftUniRestaurant.Models.Tables.Contracts
{
    public interface ITable
    {
        List<IFood> FoodOrders { get; }
        List<IDrink> DrinkOrders { get; }
        int TableNumber { get; }
        int Capacity { get; }
        int NumberOfPeople { get; }
        decimal PricePerPerson { get; }
        bool IsReserved { get; set; }
        decimal Price { get; }
    }
}
