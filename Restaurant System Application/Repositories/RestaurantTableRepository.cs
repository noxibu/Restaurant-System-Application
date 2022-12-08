using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_System_Application.Classes;
using Restaurant_System_Application.Services;

namespace Restaurant_System_Application.Repositories
{
    internal class RestaurantTableRepository
    {
        public List<RestaurantTable> RestaurantTables;

        public RestaurantTableRepository(int tableCount)
        {

            DirectoryGenerator currentDir = new DirectoryGenerator();
            List<RestaurantTable> RestaurantTables = new List<RestaurantTable>();
            StreamWriter sw = File.AppendText(currentDir.GetCurrentDirectory() + "\\RestaurantTables.csv");

            Random rand = new Random();

            for(int i = 0; i<tableCount; i++)
            {
                int seatsAvailable = rand.Next(1, 6);
                RestaurantTables.Add(new RestaurantTable(i + 1, true, 0, seatsAvailable));
                sw.WriteLine($"{i+1},true,0,{seatsAvailable}");

            }

            sw.Close();
        }
    }
}
