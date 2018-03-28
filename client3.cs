using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client1
{

    class client3
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 101);
            sck.Connect(endPoint);
            Console.WriteLine("Connected.");
            while (true)
            {
                String s = null;
                Console.WriteLine("Enter Message");
                s = Console.ReadLine();
                sck.Send(Encoding.ASCII.GetBytes(s), 0, s.Length, SocketFlags.None);

                byte[] msgfromServer = new byte[1000];
                int size = sck.Receive(msgfromServer);
                Console.WriteLine("from server " + Encoding.ASCII.GetString(msgfromServer, 0, size));
            }
        }
    }
}
