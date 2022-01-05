using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using SecurityManager;

namespace ClientApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {
                proxy.TestCommunication();
                Menu(proxy);
            }
        }

        private static void Menu(WCFClient proxy)
        {
            int option = -1;

            do
            {
                Console.WriteLine("1. Send Complaint");
                Console.WriteLine("0. Exit");

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Choose option: ");
                option = Convert.ToInt32(Console.ReadLine());

                ExecuteAction(proxy, option);
            } while (option != 0);
        }

        private static void ExecuteAction(WCFClient proxy, int option)
        {
            if (option == 1)
            {
                Console.Write("Enter your complaint: ");

                string complaint = Console.ReadLine();
                string user = proxy.GetCurrentUser();
                
                proxy.SendComplaint(user, complaint);
            }
        }
    }
}
