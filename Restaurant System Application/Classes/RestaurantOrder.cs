using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Classes
{
    internal class RestaurantOrder
    {
        public int TableId { get; set; }
        public string OrderItem { get; set; }
        //public string CustomerAction { get; set; }
        public decimal ItemPrice { get; set; }
        //public DateTime Timestamp { get; set; }


        public RestaurantOrder(int tableId, string orderItem, decimal itemPrice)
        {
            TableId = tableId;
            OrderItem = orderItem;
            ItemPrice = itemPrice;

        }


        //public RestaurantOrder(int tableId, string customerAction, DateTime timestamp)
        //{
        //    TableId = tableId;
        //    CustomerAction = customerAction;
        //    Timestamp = timestamp;

        //}
    }
}
