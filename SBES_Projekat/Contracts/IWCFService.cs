using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Net;

namespace ClientApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/WCFService";

            using (WebClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
            {
                //*
            }

            Console.ReadLine();
        }
    }
}
