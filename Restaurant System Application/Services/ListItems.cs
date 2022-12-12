using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Restaurant_System_Application.Classes;

namespace Restaurant_System_Application.Services
{
    internal class ListItems
    {
        public ListItems()
        {

        }

        public void WriteRestaurantOrderObjects(List<RestaurantOrder> list)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            using (StreamWriter sw = new StreamWriter(currentDir.GetCurrentDirectory() + "\\RestaurantOrders.csv"))
            {
                foreach(var item in list)
                {
                    //sw.WriteLine($"{item.TableId},{item.CustomerAction},{item.Timestamp}");
                    sw.WriteLine($"{item.TableId},{item.OrderItem},{item.ItemPrice}");
                    //}
                }

                sw.Close();
            }
        }

        public void WriteRestaurantTableObjects(List<RestaurantTable> list)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();

            using (StreamWriter sw = new StreamWriter(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv"))
            {
                foreach (RestaurantTable table in list)
                {
                    sw.WriteLine($"{table.TableId},{table.TableOccupied},{table.PriceTotal},{table.SeatsAvailable}");


                }

                sw.Close();
            }
        }

        public List<RestaurantTable> ListToRestaurantTableObjects(List<string> tablesReadList)
        {
            List<RestaurantTable> restaurantTablesObjects = new List<RestaurantTable>();

            foreach (string table in tablesReadList)
            {
                restaurantTablesObjects.Add(new RestaurantTable(int.Parse(table.Split(",")[0]),
                    bool.Parse(table.Split(",")[1]),
                    decimal.Parse(table.Split(",")[2]),
                    int.Parse(table.Split(",")[3])));
            }

            return restaurantTablesObjects;

        }


        public List<RestaurantOrder> ListToRestaurantOrderObjects(List<string> ordersReadList)
        {
            List<RestaurantOrder> restaurantOrdersObjects = new List<RestaurantOrder>();

            foreach (string order in ordersReadList)
            {
                try
                {
                    restaurantOrdersObjects.Add(new RestaurantOrder(int.Parse(order.Split(",")[0]),
                    order.Split(",")[1],
                    decimal.Parse(order.Split(",")[2])));
                }
                catch
                {
                    Console.WriteLine($"Cannot parse DateTime as Decimal. Value you're trying to parse is {order.Split(",")[2]}");
                }


            }

            return restaurantOrdersObjects;

        }

        public void CalculateTableTotal(int tableId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            //List<string> tablesReadList = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList();
            //List<string> ordersReadList = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantOrders.csv").ToList();
            //List<string> tableOrders = ordersReadList.Where(x => x.Split(",")[0] == tableId.ToString()).ToList();
            

            decimal tableTotal = 0;

            List<RestaurantTable> restaurantTablesObjects = ListToRestaurantTableObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList());
            List<RestaurantOrder> restaurantOrdersObjects = ListToRestaurantOrderObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantOrders.csv").ToList());

            List<RestaurantOrder> tableOrders = restaurantOrdersObjects.Where(x => x.TableId == tableId).ToList();



            Console.WriteLine(tableOrders.Count);
            List<RestaurantTable> restaurantTable = restaurantTablesObjects.Where( x => x.TableId == tableId ).ToList();
            int currentTableIndex = restaurantTablesObjects.FindIndex(x => x.TableId == tableId);

            foreach(RestaurantOrder order in tableOrders)
            {
                tableTotal += order.ItemPrice;
                Console.WriteLine("Order item price = "+order.ItemPrice);
            }

            restaurantTablesObjects[currentTableIndex].PriceTotal = tableTotal;
            

            //Write object to file.
            WriteRestaurantTableObjects(restaurantTablesObjects);








        }


        public void UpdateTable(int tableId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<string> tablesReadList = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList();
            List<RestaurantTable> restaurantTablesObjects = new List<RestaurantTable>();

            foreach(string table in tablesReadList)
            {
                restaurantTablesObjects.Add(new RestaurantTable(int.Parse(table.Split(",")[0]), 
                    bool.Parse(table.Split(",")[1]), 
                    decimal.Parse(table.Split(",")[2]), 
                    int.Parse(table.Split(",")[3])));
            }


            int elementIndex = restaurantTablesObjects.FindIndex(x => x.TableId == tableId);



            switch (restaurantTablesObjects[elementIndex].TableOccupied)
            {
                case true:
                    restaurantTablesObjects[elementIndex].TableOccupied = false;
                    break;
                case false:
                    restaurantTablesObjects[elementIndex].TableOccupied =  true;
                    break;
                default:
                    Console.WriteLine("Error: Cannot read RestaurantTables.csv data.");
                    break;
            }


            //Write object to file.
            WriteRestaurantTableObjects(restaurantTablesObjects);




        }

        public List<string> OrderMeal()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            Random rand = new Random();

            List<string> mealsFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Meals.csv").ToList();
            List<List<string>> mealsList = new List<List<string>>();
            List<string> mealOrder = new List<string>();

            foreach (string drink in mealsFile)
            {
                mealsList.Add(drink.Split(',').ToList());
            }

            mealOrder = mealsList[rand.Next(0, mealsList.Count - 1)];


            return mealOrder;
        }

        public List<string> OrderDrink()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            Random rand = new Random();

            List<string> drinksFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            List<List<string>> drinksList = new List<List<string>>();
            List<string> drinkOrder = new List<string>();

            foreach (string drink in drinksFile)
            {
                drinksList.Add(drink.Split(',').ToList());
            }

            drinkOrder = drinksList[rand.Next(0, drinksList.Count - 1)];


            return drinkOrder;
        }


        public List<List<string>> SplitListToSublists(List<string> list)
        {
            List<List<string>> splitList = new List<List<string>>();

            foreach(string element in list)
            {
                splitList.Add(element.Split(',').ToList());       
            }

            return splitList;
        }
        

        }
    }

