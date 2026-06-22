using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system
{
    internal class EmailServices
    {
        public static void SendEmail(string from, string to, string subject, string body)
        {

            Console.WriteLine($"Send email to: {to}");
            Console.WriteLine($"subject: {subject}");
            Console.WriteLine($"body: {body}");
            Console.WriteLine("Send Email successfully");
        }
}
    }
