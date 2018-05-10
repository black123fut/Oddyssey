using System.Data;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Oddyssey.Properties;

namespace Oddyssey
{
    using System;
    
    public class Program
    {

        public static void Main(string[] args)
        {
            int port = 8000;
            Client client = new Client(port);
            client.Run();
        }
    }
}