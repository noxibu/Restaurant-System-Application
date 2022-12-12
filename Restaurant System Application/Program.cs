using Restaurant_System_Application.Repositories;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RestaurantTableRepository restaurantTableRepository = new RestaurantTableRepository(10);

            RestaurantOrderGenerator generator = new RestaurantOrderGenerator();
            generator.GenerateOrders();

        }
    }
}