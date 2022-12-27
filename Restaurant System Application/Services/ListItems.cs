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

        public void ShowMenu()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<string> drinksMenu = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            List<string> mealsMenu = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            bool loop = true;

            Console.WriteLine("Choose [1] for drinks menu");
            Console.WriteLine("Choose [2] for meals menu");
            Console.WriteLine("Choose [x] to cancel");

            

            while(loop == true)
            {

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Drinks menu:");
                        foreach (string drink in drinksMenu)
                        {
                            Console.WriteLine($"{drink.Split(",")[0]}. {drink.Split(",")[1]}, {drink.Split(",")[2]} $");
                        }

                        loop = false;

                        break;
                    case "2":
                        foreach (string meal in mealsMenu)
                        {
                            Console.WriteLine($"{meal.Split(",")[0]}. {meal.Split(",")[1]}, {meal.Split(",")[2]} $");
                        }
                        loop = false;
                        break;
                    case "x":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Please choose between [1], [2] and [x]");
                        break;
                }

            }


        }

        public void ShowDrinksMenu()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            
            List<string> drinksMenu = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            Console.WriteLine("Drinks menu:");
            foreach(string drink in drinksMenu)
            {
                Console.WriteLine($"{drink.Split(",")[0]}. {drink.Split(",")[1]}, { drink.Split(",")[2]} $");
            }

        }

        public void ShowMealsMenu()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();

            List<string> mealsMenu = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            Console.WriteLine("Meals menu:");
            foreach (string meal in mealsMenu)
            {
                Console.WriteLine($"{meal.Split(",")[0]}. {meal.Split(",")[1]}, {meal.Split(",")[2]} $");
            }
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
                    sw.WriteLine($"{table.TableId},{table.TableAvailable},{table.PriceTotal},{table.SeatsAvailable}");


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
            

            decimal tableTotal = 0;

            List<RestaurantTable> restaurantTablesObjects = ListToRestaurantTableObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv").ToList());
            List<RestaurantOrder> restaurantOrdersObjects = ListToRestaurantOrderObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + $"\\{tableId}-TableOrders.csv").ToList());

            List<RestaurantOrder> tableOrders = restaurantOrdersObjects.Where(x => x.TableId == tableId).ToList();



            List<RestaurantTable> restaurantTable = restaurantTablesObjects.Where( x => x.TableId == tableId ).ToList();
            int currentTableIndex = restaurantTablesObjects.FindIndex(x => x.TableId == tableId);

            foreach(RestaurantOrder order in tableOrders)
            {
                tableTotal += order.ItemPrice;
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



            switch (restaurantTablesObjects[elementIndex].TableAvailable)
            {
                case true:
                    restaurantTablesObjects[elementIndex].TableAvailable = false;
                    break;
                case false:
                    restaurantTablesObjects[elementIndex].TableAvailable =  true;
                    break;
                default:
                    Console.WriteLine("Error: Cannot read RestaurantTables.csv data.");
                    break;
            }


            //Write object to file.
            WriteRestaurantTableObjects(restaurantTablesObjects);




        }

        public void VisitorReceipt(int tableId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<RestaurantOrder> restaurantOrdersObjects = ListToRestaurantOrderObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + $"\\{tableId}-TableOrders.csv").ToList()).Where(x => x.TableId == tableId).ToList();


            decimal totalPrice = 0;
            DateTime timestamp = DateTime.Now;
            using (StreamWriter writer = new StreamWriter(currentDir.GetCurrentDirectory() + $"\\{tableId}-TableReceipt-{timestamp.Month}-{timestamp.Day}-{timestamp.Hour}-{timestamp.Minute}.csv"))
            {
                foreach (RestaurantOrder restaurantOrder in restaurantOrdersObjects)
                {
                Console.WriteLine($"{restaurantOrder.OrderItem}, {restaurantOrder.ItemPrice}$");
                Console.WriteLine($"------------------");
                totalPrice += restaurantOrder.ItemPrice;


               
                    writer.WriteLine($"{restaurantOrder.OrderItem}, {restaurantOrder.ItemPrice}$");
                }

                writer.WriteLine($"Your total is {totalPrice}$");
            }

            

            Console.WriteLine($"Your total is: {totalPrice}$");


            CalculateTableTotal(tableId);
            UpdateTable(tableId);

            if (File.Exists(currentDir.GetCurrentDirectory() + $"{tableId}-TableOrders.csv"))
            {
                File.Delete(currentDir.GetCurrentDirectory() + $"{tableId}-TableOrders.csv");

            }




            

        }

        public void RestaurantReceipt()
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();

            List<RestaurantOrder> restaurantOrdersObjects = ListToRestaurantOrderObjects(File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\RestaurantOrders.csv").ToList());

            decimal moneyNow = 0;

            foreach(var order in restaurantOrdersObjects)
            {
                moneyNow += order.ItemPrice;
            }

            Console.WriteLine($"[{DateTime.Now}] {restaurantOrdersObjects.Count} orders, totalling to {moneyNow} $");


        }

        public void OrderDrink(int tableId, int itemId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<string> drinksFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Drinks.csv").ToList();
            List<string> drinksList = drinksFile.Where(x => x.Split(",")[0] == itemId.ToString()).ToList();

            using (FileStream tableOrder = new FileStream(currentDir.GetCurrentDirectory() + $"\\{tableId}-TableOrders.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(tableOrder))
            {
                sw.WriteLine($"{tableId},{drinksList[0].Split(",")[1]},{drinksList[0].Split(",")[2]}");
            }

            using (FileStream restaurantOrder = new FileStream(currentDir.GetCurrentDirectory() + $"\\RestaurantOrders.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(restaurantOrder))
            {
                sw.WriteLine($"{tableId},{drinksList[0].Split(",")[1]},{drinksList[0].Split(",")[2]}");
            }

            Console.WriteLine($"Table [{tableId}] ordered {drinksList[0].Split(",")[1]}.");

            CalculateTableTotal(tableId);

        }


        public void OrderMeal(int tableId, int itemId)
        {
            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<string> mealsFile = File.ReadAllLines(currentDir.GetCurrentDirectory() + "\\Meals.csv").ToList();
            List<string> mealsList = mealsFile.Where(x => x.Split(",")[0] == itemId.ToString()).ToList();

            using (FileStream tableOrder = new FileStream(currentDir.GetCurrentDirectory() + $"\\{tableId}-TableOrders.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(tableOrder))
            {
                sw.WriteLine($"{mealsList[0].Split(",")[0]},{mealsList[0].Split(",")[1]},{mealsList[0].Split(",")[2]}");
            }

            using (FileStream restaurantOrder = new FileStream(currentDir.GetCurrentDirectory() + $"\\RestaurantOrders.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(restaurantOrder))
            {
                sw.WriteLine($"{mealsList[0].Split(",")[0]},{mealsList[0].Split(",")[1]},{mealsList[0].Split(",")[2]}");
            }

            Console.WriteLine($"Table [{tableId}] ordered {mealsList[0].Split(",")[1]}.");

            CalculateTableTotal(tableId);

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
        
        public void TakeOrder()
        {
            

            Console.WriteLine("Choose [1] to order a drink");
            Console.WriteLine("Choose [2] to order a meal");
            Console.WriteLine("Choose [x] to cancel");

            bool loop = true;
            string tableId;
            string itemId;

            while(loop == true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowDrinksMenu();
                        Console.WriteLine("Enter table number:");
                        tableId = Console.ReadLine();
                        Console.WriteLine("Enter item number from the menu:");
                        itemId = Console.ReadLine();

                        try
                        {
                            OrderDrink(int.Parse(tableId), int.Parse(itemId));
                        }
                        catch
                        {
                            Console.WriteLine("Cannot take order with given parameters.");
                        }

                        loop = false;
                        
                        break;
                    case "2":
                        ShowMealsMenu();
                        Console.WriteLine("Enter table number:");
                        tableId = Console.ReadLine();
                        Console.WriteLine("Enter item number from the menu:");
                        itemId = Console.ReadLine();

                        try
                        {
                            OrderMeal(int.Parse(tableId), int.Parse(itemId));
                            
                        }
                        catch
                        {
                            Console.WriteLine("Cannot take order with given parameters.");

                        }

                        loop = false;

                        break;
                    case "x":
                        Console.WriteLine("Cancelling...");
                        loop = false;
                        break;

                    default:
                        Console.WriteLine("Choose between [1], [2] and [x]");
                        break;
                }
            }

        }


        }
    }

