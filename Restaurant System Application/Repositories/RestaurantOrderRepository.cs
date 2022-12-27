using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_System_Application.Classes;

namespace Restaurant_System_Application.Repositories
{
    internal class RestaurantOrderRepository
    {
        List<RestaurantOrder> RestaurantOrders;


        public RestaurantOrderRepository()
        {
            RestaurantOrders = new List<RestaurantOrder>();


        }
    }
}
