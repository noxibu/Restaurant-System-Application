using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_System_Application.Classes;
using Restaurant_System_Application.Services;
using Restaurant_System_Application.Repositories;

namespace Restaurant_System_Application.Services
{
    internal class RestaurantOrderGenerator
    {

        public RestaurantOrderGenerator()
        {

        }

        public void GenerateOrders()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            
            ListItems listItems = new ListItems();

            Random rand = new Random();

            List<string> restaurantTablesFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList();
            List<string> restaurantMealsFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Meals.csv").ToList();
            List<string> restaurantDrinksFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            

            List<List<string>> tablesList = listItems.SplitListToSublists(restaurantTablesFile);
            List<List<string>> mealsList= listItems.SplitListToSublists(restaurantMealsFile);
            List<List<string>> drinksList= listItems.SplitListToSublists(restaurantDrinksFile);

            List<string> orderItem = new List<string>();
            int tableId;
            decimal tableTotal;
            int seatsAvailable;
            DateTime timeStart;
            DateTime timeEnd;
            int visitorsNum;


            foreach(var table in tablesList)
            {
                
                timeStart = DateTime.Now.AddMinutes(rand.Next(0, 59));
                tableId = Int32.Parse(table[0]);
                tableTotal = decimal.Parse(table[2]);
                seatsAvailable = int.Parse(table[3]);
                visitorsNum = rand.Next(1, 6);


                //Check if table is occupied.
                // If not occupied, change state and take orders.
                if (bool.Parse(table[1]) != false && visitorsNum <= seatsAvailable)
                {

                    StreamWriter sw = File.AppendText(currentDir.GetCurrentDirectory() + "\\RestaurantOrders.csv");
                    
                    //Customer arrives.
                    sw.WriteLine($"{tableId},Arrival,{timeStart}");
                    listItems.UpdateTable(tableId);

                    
                    for (int i = 0; i < rand.Next(2, 8); i++)
                    {
                        switch (rand.Next(0, 2))
                        {
                            case 0:
                                orderItem = listItems.OrderDrink();
                                Console.WriteLine($"Table[{tableId}] ordered a drink");
                                break;

                            case 1:
                                orderItem = listItems.OrderMeal();
                                Console.WriteLine($"Table[{tableId}] ordered a meal");
                                break;
                            default:
                                Console.WriteLine("Didn't order anything");
                                break;
                        }
                        sw.WriteLine($"{tableId},{orderItem[0]},{orderItem[1]}");

                    }

                    //Customer leaves.

                    timeEnd = timeStart.AddHours(rand.Next(0, 2)).AddMinutes(rand.Next(0, 59));
                    sw.WriteLine($"{tableId},Departure,{timeEnd}");
                    listItems.UpdateTable(tableId);
                    listItems.CalculateTableTotal(tableId);



                    sw.Close();
                }

                
            }








        }

    }
}
