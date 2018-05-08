using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Oddyssey
{
    using System;
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            //casa: 192.168.100.6
            TcpClient client = new TcpClient("192.168.100.6", 8000);

            //Convierte a String el Xml
            String xml = Serialize(WriteXml()).Replace("\n", String.Empty);

            byte[] buf;
            buf = Encoding.UTF8.GetBytes(xml);
            
            NetworkStream stream = client.GetStream();
            stream.Write(buf, 0, buf.Length);

//            byte[] bytes = new byte[256];
//            Int32 response = stream.Read(bytes, 0, bytes.Length);          

        }

        public static string Serialize(XmlDocument xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(xml.GetType());

            using (StringWriter text = new StringWriter())
            {
                xmlSerializer.Serialize(text, xml);
                return text.ToString();
            }

        }

        public static XmlDocument WriteXml()
        {
            XmlDocument xml = new XmlDocument();
            XmlNode rootNode = xml.CreateElement("Message");
            xml.AppendChild(rootNode);

            XmlNode opcode = xml.CreateElement("opcode");
            opcode.InnerText = "Registrar";
            rootNode.AppendChild(opcode);

            XmlNode data = xml.CreateElement("Data");
            XmlNode username = xml.CreateElement("username");
            username.InnerText = "Hojo";
            XmlNode name = xml.CreateElement("name");
            name.InnerText = "Isaac";
            XmlNode surname = xml.CreateElement("surname");
            surname.InnerText = "Benavides";
            XmlNode age = xml.CreateElement("age");
            age.InnerText = "19";
            
            data.AppendChild(username);
            data.AppendChild(name);
            data.AppendChild(surname);
            data.AppendChild(age);

            rootNode.AppendChild(data);
            return xml;
        }
    }
}