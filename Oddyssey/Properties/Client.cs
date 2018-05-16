using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using NAudio.Wave;


namespace Oddyssey.Properties
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private XmlDocument message;
        
        public Client()
        {
            //Casa  192.168.100.6
            tcpClient = new TcpClient("192.168.100.6", 8000);

            //Convierte a String el Xml
            stream = tcpClient.GetStream(); 
            message = new XmlDocument();
        }


        public void Send(XmlDocument message)
        {
            StreamWriter writer = new StreamWriter(stream);
            String xml = Serialize(message).Replace("\n", String.Empty);

            writer.WriteLine(xml);
            writer.Flush();
            
            Receive();
        }
        

        public void Receive() 
        {
            StreamReader reader = new StreamReader(stream);
            String data = reader.ReadLine();
            
            //Todavia no lo lee como xml pero sí como string 
            if (data != null)
            {
                message.LoadXml(data);
                
                String opcode = message.SelectSingleNode("Message/opcode").InnerText;
                String bytes = message.SelectSingleNode("Message/Data/bytes").InnerText;

                byte[] toStream = Convert.FromBase64String(bytes);

                byte[] copy = File.ReadAllBytes("torero.mp3");
                         
                using (var mp3Stream = new MemoryStream(toStream))
                {
                    using (var mp3FileReader = new Mp3FileReader(mp3Stream))
                    {
                        using (var wave32 = new WaveChannel32(mp3FileReader, 0.1f, 1f))
                        {
                            using (var ds = new DirectSoundOut())
                            {
                                ds.Init(wave32);
                                ds.Play();
                                Thread.Sleep(30000);
                            }
                        }
                    }
                }
                
//                IWaveProvider provider = new RawSourceWaveStream(
//                    new MemoryStream(copy), new WaveFormat());
//                var waveOut = new WaveOut();
//                waveOut.Init(provider);
//                waveOut.Play();                

//                MediaPlayer mediaPlayer = new MediaPlayer(copy);
//                mediaPlayer.Play(copy);
//                while (true)
//                {
//                    Console.Write("");
//                }
            }
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

        public XmlDocument SignInMessage(String username, String name, String surname, int age)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode rootNode = xml.CreateElement("Message");
            xml.AppendChild(rootNode);

            XmlNode opcode = xml.CreateElement("opcode");
            opcode.InnerText = "004";
            rootNode.AppendChild(opcode);

            XmlNode data = xml.CreateElement("Data");
            XmlNode username1 = xml.CreateElement("username");
            username1.InnerText = username;
            XmlNode name1 = xml.CreateElement("name");
            name1.InnerText = name;
            XmlNode surname1 = xml.CreateElement("surname");
            surname1.InnerText = surname;
            XmlNode age1 = xml.CreateElement("age");
            age1.InnerText = age.ToString();
            
            data.AppendChild(username1);
            data.AppendChild(name1);
            data.AppendChild(surname1);
            data.AppendChild(age1);

            rootNode.AppendChild(data);
            Send(xml);
            
            return xml;
        }

        public XmlDocument GetMessage()
        {
            return message;
        }
    }
    
    public class MediaPlayer
    {
        System.Media.SoundPlayer soundPlayer;

        public MediaPlayer(byte[] buffer)
        {
            var memoryStream = new MemoryStream(buffer, true);
            soundPlayer = new System.Media.SoundPlayer(memoryStream);
        }

        public void Play()
        {
            soundPlayer.Play();
        }

        public void Play(byte[] buffer)
        {
            soundPlayer.Stream.Seek(0, SeekOrigin.Begin);
            soundPlayer.Stream.Write(buffer, 0, buffer.Length);
            soundPlayer.Play();
        }
    }
}