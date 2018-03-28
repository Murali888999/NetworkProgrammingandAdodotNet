using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace Server
{
    class MultiServer
    {
        static Dictionary<string, ClientHandler> dSockets = new Dictionary<string, ClientHandler>();
        static int i = 1;
        static Socket sck;
        static Socket acc;

        static void Connection()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234));
            sck.Listen(2);
        }

        static void Operation()
        {
            while (true)
            {
                // Accept the incoming request
                acc = sck.Accept();
                try
                {

                    byte[] Buf = new byte[255];
                    int rec = acc.Receive(Buf, 0, Buf.Length, 0);
                    Array.Resize(ref Buf, rec);
                    string dis = Encoding.Default.GetString(Buf);
                    Console.WriteLine(dis + " Joined.....");
                    byte[] send = Encoding.Default.GetBytes(dis);
                    acc.Send(send, 0, send.Length, 0);
                    byte[] Buf1 = new byte[255];
                    int rec1 = acc.Receive(Buf1, 0, Buf1.Length, 0);
                    Array.Resize(ref Buf1, rec1);
                    string dis1 = Encoding.Default.GetString(Buf1);

                    ClientHandler mtch = new ClientHandler(acc, dis, dis1, dSockets);
                    dSockets.Add(dis, mtch);

                    Thread t = new Thread(mtch.Run);
                    Thread t1 = new Thread(mtch.Send);
                    t.Start();
                    t1.Start();
                    i++;
                }
                catch
                {
                    Console.WriteLine("Hello");
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("WelCome To Chat Server");
            Connection();
            Operation();

        }

    }
}