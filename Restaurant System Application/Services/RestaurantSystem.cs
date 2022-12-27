using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_System_Application.Classes;
using Restaurant_System_Application.Repositories;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application.Services
{
    internal class RestaurantSystem
    {

        public RestaurantSystem()
        {

        }

        public void StartSystem()
        {
            // Accomodate visitors
            // Show available tables
            // Show menu
            // Show drinks menu
            // Show meals menu
            // Take order
            // Take meal order
            // Take drink order
            // Print receipt for visitor
            // Print restaurant receipt

            RestaurantOrderGenerator orderGenerator = new RestaurantOrderGenerator();
            ListItems listItems = new ListItems();



            Console.WriteLine("Starting J-Keeper...");


            bool quit = false;
            int visitorNum = 0;
            int tableId = 0;


            while (quit != true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("[1] Show available tables");
                Console.WriteLine("[2] Accomodate visitors");
                Console.WriteLine("[3] Show menu");
                Console.WriteLine("[4] Take order");
                Console.WriteLine("[5] Print guests' receipt");
                Console.WriteLine("[6] Print restaurant receipt");
                Console.WriteLine("[quit] Exit J-Keeper");
                Console.WriteLine("\n");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Enter guest number:");
                        visitorNum = int.Parse(Console.ReadLine());
                        orderGenerator.SeeAvailableTables(visitorNum);

                        break;
                    case "2":
                        Console.WriteLine("Enter guest number:");
                        visitorNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter table number:");
                        tableId = int.Parse(Console.ReadLine());

                        orderGenerator.SelectTable(tableId, visitorNum);
                        break;
                    case "3":
                        listItems.ShowMenu();
                        break;
                    case "4":
                        Console.WriteLine("Taking order...");
                        listItems.TakeOrder();

                        break;
                    case "5":
                        Console.WriteLine("Enter table ID.");
                        tableId = int.Parse(Console.ReadLine());
                        Console.WriteLine("\n");

                        listItems.VisitorReceipt(tableId);
                        break;
                    case "6":

                        listItems.RestaurantReceipt();

                        break;
                    case "quit":
                        Console.WriteLine("Exiting J-Keeper.");
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("ERROR. Exiting system.");
                        quit = true;
                        break;

                }
            }



        }

    }


}
