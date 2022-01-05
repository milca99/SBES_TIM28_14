using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Contracts;

namespace SecurityManager
{
    public class ClientCertValidator : X509CertificateValidator
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
            /*  List<string> proba = new List<string>();
              string xml = @"C:\Users\hp\Desktop\FAKS\SBES\Vezbe\Vezba_03_resenje\ServiceApp\banned_certs.xml"; // IZMENITI!!!
              proba = XMLHelper.ReadXml(xml); 
              foreach (string s in proba)
              {
                  if (certificate.Subject.Equals(s))
                  {
                      throw new Exception("Certificate is forbidden.");
                  }

              } */
        }
    }
}
