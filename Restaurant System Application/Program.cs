using Restaurant_System_Application.Repositories;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantTableRepository restaurantTableRepository = new RestaurantTableRepository(10);

            RestaurantOrderGenerator generator = new RestaurantOrderGenerator();
            generator.GenerateOrders();

            //string parseable = "77.7";
            //string unparseable = "contains";

            //for(int i = 0; i<5; i++)
            //{
            //    try
            //    {
            //        Console.WriteLine(decimal.Parse(unparseable));
            //    }
            //    catch
            //    {
            //        Console.WriteLine($"{i+1}.Cannot parse");
            //    }
            //}

        }
    }
}