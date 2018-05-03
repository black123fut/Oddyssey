using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace Oddyssey
{
    using System;
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            TcpClient client = new TcpClient("192.168.100.6", 8000);

            String xml = Register();

            byte[] buf;
            buf = Encoding.UTF8.GetBytes(xml);

            NetworkStream stream = client.GetStream();
            stream.Write(buf, 0, xml.Length);
        }

        public static String Register()
        {
            Console.Write("Username: ");
            String username = Console.ReadLine();
            
            Console.Write("Name: ");
            String name2 = Console.ReadLine();
            String name = Console.ReadLine();

            Console.Write("Surname: ");
            String surname2 = Console.ReadLine();
            String surname = Console.ReadLine();

            Console.Write("Age: ");
            String surname3 = Console.ReadLine();
            String age = Console.ReadLine();
            
            String xml = "<message><command>" + "registrar" + "</command>" +
                         "<data><username>" + username + "</username>" + 
                         "<name>" + name + "</name>" + 
                         "<surname>" + surname + "</surname>" + 
                         "<age>" + age + "</age></data>" + 
                         "</message>";
            return xml;
        }
    }
}