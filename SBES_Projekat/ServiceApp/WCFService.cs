using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using SecurityManager;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

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

            if (ComplaintContainsBannedWord(complaint))
            {
                Console.Write($"Complaint contains banned word. {user} has been banned.");
                // TODO: Add user to banned_certs.xml
            } else
            {
                SaveComplaint(user, complaint);
            }
        }

        public void BanTheUser(string username)
        {
            XMLHelper.WriteXML(username);
        }

        private bool ComplaintContainsBannedWord(string complaint)
        {
            try
            {
                string fileName = @"banned_words.txt";

                foreach (string bannedWord in File.ReadLines(fileName, Encoding.UTF8))
                {
                    if (complaint.Contains(bannedWord))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[ComplaintContainsBannedWord] ERROR = {0}", e.Message);
                return false;
            }

            return false;
        }

        private void SaveComplaint(string user, string complaint)
        {
            try
            {
                string fileName = @"complaints.txt";

                using (StreamWriter stream = new StreamWriter(fileName, true))
                {
                    stream.WriteLine($"{user},{complaint}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[SaveComplaint] ERROR = {0}", e.Message);
            }
        }
    }
}
