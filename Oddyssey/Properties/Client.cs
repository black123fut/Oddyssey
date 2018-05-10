using System;
using System.IO;
using System.Media;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Oddyssey.Properties
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        
        public Client(int port)
        {
            client = new TcpClient("192.168.100.6", port);

            //Convierte a String el Xml
            stream = client.GetStream(); 
        }

        public void Run()
        {
            
                Send();
                Receive();
            
        }

        public void Send()
        {
            StreamWriter writer = new StreamWriter(stream);
            String xml = Serialize(WriteXml()).Replace("\n", String.Empty);

            writer.WriteLine(xml);
            writer.Flush();
        }

        public void Receive()
        {
            StreamReader reader = new StreamReader(stream);
            
            String data = reader.ReadLine();
            Console.WriteLine(data);
//            byte[] mp3 = new byte[1024];
//            
//            Console.Write(mp3);
//            
//            for (int i = 0; i < bytes; i++)
//            {
//                mp3[i] = receive[i];
//            }
            
//            byte[] bytes = new byte[1024];
//            Int32 response = stream.Read(bytes, 0, bytes.Length);
//            byte[] mp3;
//            char[] hear = new char[1024];
//            mp3 = Encoding.UTF8.GetBytes(hear, 0, response);
            
//            Stream buff = new MemoryStream(mp3);
//            SoundPlayer sp = new SoundPlayer(buff);
//            sp.Play();  
//            using (MemoryStream ms = new MemoryStream(mp3))
//            {
//                SoundPlayer player = new SoundPlayer(ms);
//                player.Play();
//            }
        }

        public string Serialize(XmlDocument xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(xml.GetType());

            using (StringWriter text = new StringWriter())
            {
                xmlSerializer.Serialize(text, xml);
                return text.ToString();
            }

        }

        public XmlDocument WriteXml()
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