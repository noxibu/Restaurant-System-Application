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
        public bool TableOccupied { get; set; }
        public decimal PriceTotal { get; set; }
        public int SeatsAvailable { get; set; }


        public  RestaurantTable(int tableId, bool tableOccupied, decimal priceTotal, int seatsAvailable)
        {
            TableId = tableId;
            TableOccupied = tableOccupied;
            PriceTotal = priceTotal;
            SeatsAvailable = seatsAvailable;
        }
    }
}
