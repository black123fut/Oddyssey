
using System.IO;
using Oddyssey.Properties;

namespace Oddyssey
{
    using System;
    
    public class Program
    {

        public static void Main(string[] args)
        {
            String archivo = "Rick Astley - Never Gonna Give You Up.mp3";
            TagLib.File file = TagLib.File.Create(archivo);
            byte[] copy = File.ReadAllBytes(archivo);

//            Console.WriteLine(file.Tag.Lyrics);
//            Console.WriteLine(file.Tag.FirstPerformer);
//            if (file.Tag.Title == null)
//            {
//                Console.Write("LOL");
//            }
//            Console.WriteLine(file.Tag.Album);
            Client client = new Client();
            client.SendSongMessage(archivo);
        }
    }
}