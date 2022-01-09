﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using SecurityManager;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using System.ServiceModel;

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
                
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void BanTheUser(string username)
        {
            XMLHelper.WriteXML(username);


            Audit.BanTheUserSuccess(username);
                    
            
        }
        public void Forgive(string user)
        {
            // dodati da brise iz xml fajla

            Audit.ForgiveSuccess(user);


        }

        public List<string> ListComplaintsWithBannedWords()
        {
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
