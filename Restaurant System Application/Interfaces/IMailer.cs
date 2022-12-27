using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Interfaces
{
    internal interface IMailer
    {
        string AddressFrom { get; set; }
        string AddressTo { get; set; }
        string MailSubject { get; set; }
        string MailBody { get; set; }


        void SendMail();
    }
}
