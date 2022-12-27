using Restaurant_System_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Services
{
    internal class Mailer : IMailer
    {
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public string MailSubject {get; set; }
        public string MailBody { get; set; }

        public void SendMail()
        {
            Console.WriteLine("Enter sender e-mail address:");
            string addressFrom = Console.ReadLine();
            Console.WriteLine("Enter recipients e-mail address:");
            string addressTo = Console.ReadLine();
            Console.WriteLine("Enter e-mail subject:");
            string mailSubject = Console.ReadLine();
            Console.WriteLine("Enter e-mail body:");
            string mailBody = Console.ReadLine();

            Console.WriteLine("Sending e-mail...");

            Console.WriteLine($"Sent by: {addressFrom}");
            Console.WriteLine($"Sent to: {addressTo}");
            Console.WriteLine($"Subject: {mailSubject}");
            Console.WriteLine($"Body: \n{mailBody}");

        }

        public Mailer() { }

    }
}
