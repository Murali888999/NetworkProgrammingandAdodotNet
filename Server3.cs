using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server3
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Any, 101));
            sck.Listen(1);
            Console.WriteLine("socket listing........");

            Socket Client = default(Socket);
            int count = 0;
            Server3 s = new Server3();
            while (true)
            {
                count++;
                Client = sck.Accept();
                Console.WriteLine(count + "connected........");
                Thread userthread1 = new Thread(new ThreadStart(() => s.user(Client)));
                userthread1.Start();
            }
        }
        private void user(Socket client)
        {
            while (true)
            {
                byte[] readb = new byte[1000];
                int data = client.Receive(readb);
                String s1 = Encoding.ASCII.GetString(readb, 0, data);
                Console.WriteLine(s1);
                client.Send(readb, 0, data, SocketFlags.None);
            }
        }
    }
}
