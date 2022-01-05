using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using SecurityManager;
using System.Security.Cryptography.X509Certificates;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }

    }
}
