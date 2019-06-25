using SoftUniRestaurant.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Models
{
    public class TableFactory
    {
        public Table CreateTable(string type, int tableNumber, int capacity)
        {
            Table table = null;

            if (type == "Inside")
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else if (type == "Outside")
            {
                table = new OutsideTable(tableNumber, capacity);
            }

            return table;
        }
    }
}
