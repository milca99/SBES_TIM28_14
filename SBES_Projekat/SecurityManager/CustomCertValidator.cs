using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Contracts;

namespace SecurityManager
{
    public class CustomCertValidator : X509CertificateValidator
    {
        /// <summary>
        /// Implementation of a custom certificate validation on the client side.
        /// Client should consider certificate valid if the given certifiate is not self-signed.
        /// If validation fails, throw an exception with an adequate message.
        /// </summary>
        /// <param name="certificate"> certificate to be validate </param>
        public override void Validate(X509Certificate2 certificate)
        {
            if (certificate.Subject.Equals(certificate.Issuer))
            {
                throw new Exception("Certificate is self-issued.");
            }

            List<string> bannedUsers = new List<string>();
            string xml = @"banned_certs.xml";
            bannedUsers = XMLHelper.ReadXml(xml);

            Console.WriteLine("Banned users are: ");
            foreach (string bannedUser in bannedUsers)
            {
                Console.WriteLine(bannedUser);
                if (certificate.SubjectName.Name.Contains(string.Format("CN={0}", bannedUser)))
                {
                    Console.WriteLine($"{bannedUser} - this user has been banned!");
                    throw new Exception("Certificate is forbidden.");
                }
            }
        }
    }
}
