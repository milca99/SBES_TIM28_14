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
            SaveComplaint(user, complaint);

        }


        public void BanTheUser(string username)
        {
            XMLHelper.WriteXML(username);
        }
        public void Forgive()
        {
            // nista, ili neki ispis dodati, ili eventualno da brise iz fajla
        }

        public Dictionary<string, string> ListComplaintsWithBannedWords()
        {

            Dictionary<string, string> listBanned = new Dictionary<string, string>();
            foreach (var d in Database.complaints)
            {
                if(ComplaintContainsBannedWord(d.Value))
                {
                    listBanned.Add(d.Key, d.Value);
                }
                
            }
            if (listBanned.Count.Equals(0))
            {
                return null;
            }
            return listBanned;

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
            //List<Dictionary<string, string>> complaints = new List<Dictionary<string, string>>();
            Database.complaints.Add(user, complaint);
        }
    }
}
