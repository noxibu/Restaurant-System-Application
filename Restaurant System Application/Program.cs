using Restaurant_System_Application.Repositories;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RestaurantSystem jKeeper = new RestaurantSystem();

            jKeeper.StartSystem();

            //Cannot create xUnit test

        }
    }
}