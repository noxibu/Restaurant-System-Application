using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Classes
{
    internal class ReceiptGuest
    {
        public int TableId { get; set; }
        public List<List<string>> ItemsOrdered { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime TimeArrival { get; set; }
        public DateTime TimeDeparture { get; set; }

        public ReceiptGuest(int tableId, List<List<string>> itemsOrdered, decimal orderTotal, DateTime timestamp, DateTime timeDeparture)
        {
            TableId = tableId;
            ItemsOrdered = itemsOrdered;
            OrderTotal = orderTotal;
            TimeArrival = timestamp;
            TimeDeparture = timeDeparture;
        }
    }
}
