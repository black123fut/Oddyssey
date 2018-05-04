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
               


            XmlDocument docxml = new XmlDocument();
            XmlNode rootNode = docxml.CreateElement("users");
            docxml.AppendChild(rootNode);

            XmlNode userNode = docxml.CreateElement("user");
            XmlAttribute attributeUserName = docxml.CreateAttribute("username");
            XmlAttribute attributeAge = docxml.CreateAttribute("Age");
            attributeUserName.Value = "black123fut";
            attributeAge.Value = "19";
            userNode.Attributes.Append(attributeUserName);
            userNode.Attributes.Append(attributeAge);

            userNode.InnerText = "Isaac";
            rootNode.AppendChild(userNode);
            docxml.Save("test-doc.xml");
            
            
            
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