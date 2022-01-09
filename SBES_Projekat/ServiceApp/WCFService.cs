using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using SecurityManager;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using System.ServiceModel;
using System.Threading;
using System.Security.Principal;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");

        }

        public void SendComplaint(string user, string complaint, byte[] sign)
        {
            /*
            if (Thread.CurrentPrincipal.IsInRole("Korisnik"))
            {
            */
                try
                {
                    string clienName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

                    string clientNameSign = clienName + "_sign";
                    X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
                        StoreLocation.LocalMachine, clientNameSign);

                    /// Verify signature using SHA1 hash algorithm
                    if (DigitalSignature.Verify(complaint, HashAlgorithm.SHA1, sign, certificate))
                    {
                        Console.WriteLine($"User: {user} has sent following complaint: {complaint}.");
                        SaveComplaint(user, complaint);

                    }
                    else
                    {
                        Console.WriteLine("Sign is invalid");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            /*
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call SendComplaint method (time : {1}). " +
                    "For this method need to be member of group Korisnik.", name, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
            */
        }


        public void BanTheUser(string username)
        {
            /*
            if (Thread.CurrentPrincipal.IsInRole("Nadzor"))
            {
            */
                XMLHelper.WriteXML(username);
                Audit.BanTheUserSuccess(username);
            /*
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call BanTheUser method (time : {1}). " +
                    "For this method need to be member of group Nadzor.", name, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
           */
        }
        public void Forgive(string user)
        {
            /*
            if (Thread.CurrentPrincipal.IsInRole("Nadzor"))
            {
            */
                XMLHelper.RemoveUser(user);
                Audit.ForgiveSuccess(user);
            /*
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call Forgive method (time : {1}). " +
                    "For this method need to be member of group Nadzor.", name, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
           */
        }

        public List<string> ListComplaintsWithBannedWords()
        {
            /*
            if (Thread.CurrentPrincipal.IsInRole("Nadzor"))
            {
            */
                try
                {
                    List<string> bannedComplaints = new List<string>();
                    List<string> complaints = Database.GetComplaints();

                    foreach (var complaint in complaints)
                    {
                        if (ComplaintContainsBannedWord(complaint))
                        {
                            bannedComplaints.Add(complaint);
                        }

                    }
                    if (bannedComplaints.Count.Equals(0))
                    {
                        return null;
                    }
                    return bannedComplaints;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            /*
            }
            else
            {
                string name = Thread.CurrentPrincipal.Identity.Name;
                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call ListComplaintsWithBannedWords method (time : {1}). " +
                    "For this method need to be member of group Nadzor.", name, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
           */
        }

        private bool ComplaintContainsBannedWord(string complaint)
        {
            try
            {
                string fileName = @"banned_words.txt";

                foreach (string bannedWord in File.ReadLines(fileName, Encoding.UTF8))
                {
                    if (complaint.ToLower().Contains(bannedWord.ToLower()))
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
