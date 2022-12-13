using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Classes
{
    internal class RestaurantTable
    {
        public int TableId { get; set; }
        public bool TableAvailable { get; set; }
        public decimal PriceTotal { get; set; }
        public int SeatsAvailable { get; set; }


        public  RestaurantTable(int tableId, bool tableAvailable, decimal priceTotal, int seatsAvailable)
        {
            TableId = tableId;
            TableAvailable = tableAvailable;
            PriceTotal = priceTotal;
            SeatsAvailable = seatsAvailable;
        }
    }
}
