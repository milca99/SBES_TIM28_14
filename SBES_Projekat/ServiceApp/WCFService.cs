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

        public void SendComplaint(string user, string complaint)
        {
            Console.WriteLine($"User: {user} has sent following complaint: {complaint}.");

            // TODO: Check if complaint contains banned words

            // TODO: Ban user if complaint contains one of the banned words.
        }
    }
}
