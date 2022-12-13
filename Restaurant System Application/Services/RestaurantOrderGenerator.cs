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

        public void SeeAvailableTables(int visitorsNum)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<List<int>> availableTables = new List<List<int>>();
            List<string> restaurantTables = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList();

            List<string> unoccupiedTables = restaurantTables.Where(x => bool.Parse(x.Split(",")[1]) == true && int.Parse(x.Split(",")[3]) >= visitorsNum).ToList();

            if(unoccupiedTables.Count > 0)
            {
                foreach (string table in unoccupiedTables)
                {
                    Console.WriteLine($"Table [{table.Split(",")[0]}] is available for {visitorsNum} visitor(s).");

                    //availableTables.Add(new List<int> { int.Parse(table.Split(",")[0]), int.Parse(table.Split(",")[3]) });
                }
            } else
            {
                Console.WriteLine($"There are no available tables for {visitorsNum} guest(s).");
            }

            
        }

        public void SelectTable(int tableId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<string> restaurantTables = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList();
            ListItems listItems = new ListItems();
            List<RestaurantTable> restaurantTableObjects = listItems.ListToRestaurantTableObjects(restaurantTables);


            int elementIndex = restaurantTableObjects.FindIndex(x => x.TableId == tableId);

            restaurantTableObjects[elementIndex].TableAvailable = !restaurantTableObjects[elementIndex].TableAvailable;
            listItems.WriteRestaurantTableObjects(restaurantTableObjects);

            if(restaurantTableObjects[elementIndex].TableAvailable == false)
            {
                Console.WriteLine($"Table [{tableId}] is now occupied.");
            } else if(restaurantTableObjects[elementIndex].TableAvailable == true)
            {
                Console.WriteLine($"Table [{tableId}] is now available.");
            } else
            {
                Console.WriteLine("Error: Cannot change table status.");
            }

        }






        public void GenerateOrders()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            
            ListItems listItems = new ListItems();

            Random rand = new Random();

            List<string> restaurantTablesFile;
            List<string> restaurantMealsFile;
            List<string> restaurantDrinksFile;
            

            List<List<string>> tablesList = listItems.SplitListToSublists(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList());
            List<List<string>> mealsList= listItems.SplitListToSublists(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Meals.csv").ToList());
            List<List<string>> drinksList= listItems.SplitListToSublists(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList());

            List<string> orderItem = new List<string>();
            int tableId;
            decimal tableTotal;
            int seatsAvailable;
            DateTime timeStart;
            DateTime timeEnd;
            int visitorsNum;

            List<RestaurantOrder> restaurantOrdersObjects = new List<RestaurantOrder>();

            foreach (var table in tablesList)
            {
                
                timeStart = DateTime.Now.AddMinutes(rand.Next(0, 59));
                tableId = Int32.Parse(table[0]);
                tableTotal = decimal.Parse(table[2]);
                seatsAvailable = int.Parse(table[3]);
                visitorsNum = rand.Next(1, 6);


                //Check if table is occupied or seats available for visitorsNum
                // If not occupied, change state and take orders.
                if (bool.Parse(table[1]) != false && visitorsNum <= seatsAvailable)
                {


                    

                    //Customer arrives.
                    
                    listItems.UpdateTable(tableId);

                    
                    for (int i = 0; i < rand.Next(2, 8); i++)
                    {
                        switch (rand.Next(0, 2))
                        {
                            case 0:
                                orderItem = listItems.OrderDrink();
                                Console.WriteLine($"Table[{tableId}] ordered a drink, {orderItem[0]},{orderItem[1]}");
                                break;

                            case 1:
                                orderItem = listItems.OrderMeal();
                                Console.WriteLine($"Table[{tableId}] ordered a meal, {orderItem[0]},{orderItem[1]}");
                                break;
                            default:
                                Console.WriteLine("Didn't order anything");
                                break;
                        }
                        restaurantOrdersObjects.Add(new RestaurantOrder(tableId, orderItem[0], decimal.Parse(orderItem[1])));

                    }

                    //Customer leaves
                    listItems.UpdateTable(tableId);
                    listItems.WriteRestaurantOrderObjects(restaurantOrdersObjects);
                    listItems.CalculateTableTotal(tableId);
                } else
                {
                    Console.WriteLine($"Couldn't seat {visitorsNum} people on table NO.{tableId} which seats {seatsAvailable} people.");
                }

                
            }


            








        }

    }
}
