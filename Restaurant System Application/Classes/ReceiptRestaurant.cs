using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Classes
{
    internal class ReceiptRestaurant
    {
        public int OrdersServed { get; set; }
        public decimal OrdersTotal { get; set; }

        public ReceiptRestaurant(int ordersServed, decimal ordersTotal)
        {
            OrdersServed = ordersServed;
            OrdersTotal = ordersTotal;
        }
    }
}
