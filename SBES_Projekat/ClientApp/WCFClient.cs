using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;
using SecurityManager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace ClientApp
{
    public class WCFClient : ChannelFactory<IWCFService>, IWCFService, IDisposable
    {
        IWCFService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();


        }

        public void TestCommunication()
        {
            try
            {
                factory.TestCommunication();
            }
            catch (Exception e)
            {

                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }
        public void BanTheUser(string username)
        {
            try
            {
                factory.BanTheUser(username);
            }
            catch (Exception e)
            {

                Console.WriteLine("[BanTheUser] ERROR = {0}", e.Message);
            }
        }
        public void Forgive()
        {
            try
            {
                factory.Forgive();
            }
            catch(Exception e)
            {
                Console.WriteLine("[Forgive] ERROR = {0}", e.Message);
            }
        }
        public List<string> ListComplaintsWithBannedWords()
        {
            try
            {
                return factory.ListComplaintsWithBannedWords();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ListComplaintsWithBannedWords] ERROR = {0}", e.Message);
                return null;
            }
        }
        public void SendComplaint(string user, string complaint)
        {
            try
            {
                factory.SendComplaint(user, complaint);
            }
            catch (Exception e)
            {
                Console.WriteLine("[SendComplaint] ERROR = {0}", e.Message);
            }
        }

        public string GetCurrentUser()
        {
            return Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }
    }
}
