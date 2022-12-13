using Restaurant_System_Application.Repositories;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RestaurantTableRepository restaurantTableRepository = new RestaurantTableRepository(10);

            ListItems listItems = new ListItems();
            RestaurantOrderGenerator generator = new RestaurantOrderGenerator();
            //generator.GenerateOrders();
            
            generator.SeeAvailableTables(8);

            //foreach(var table in availableTableIds)
            //{
            //    Console.WriteLine($"Table number {table[0]} is available and seats {table[1]} person(s).");
            //}

            //listItems.UpdateTable(1);

            //listItems.OrderMeal(2, 4);
            listItems.OrderDrink(2, 4);


            //listItems.ShowMealsMenu();
            //generator.SelectTable(5);


        }
    }
}