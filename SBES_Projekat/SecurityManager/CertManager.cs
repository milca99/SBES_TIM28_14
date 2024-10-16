﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Security.Principal;


namespace SecurityManager
{
    public class CertManager
    {
        /// <summary>
        /// Get a certificate with the specified subject name from the predefined certificate storage
        /// Only valid certificates should be considered
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="storeLocation"></param>
        /// <param name="subjectName"></param>
        /// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            //subjectName = "testservis";
            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            string name = String.Empty;
            /// Check whether the subjectName of the certificate is exactly the same as the given "subjectName"
            foreach (X509Certificate2 c in certCollection)
            {
                name = CertManager.GetCertName(c.Subject);

                if (name.Equals("CN=" + subjectName.ToLower()))
                {
                    return c;
                }
            }

            return null;
        }

        public static string GetCertName(string n)
        {
            string name = String.Empty;

            if (n.Contains(','))
            {
                return name = n.Split(',')[0];
            }
            else
            {
                return name = n;
            }
        }


        /// <summary>
        /// Get a certificate from file.		
        /// </summary>
        /// <param name="fileName"> .cer file name </param>
        /// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
        public static X509Certificate2 GetCertificateFromFile(string fileName)
        {
            X509Certificate2 certificate = null;


            return certificate;
        }

        /// <summary>
        /// Get a certificate from file.
        /// </summary>
        /// <param name="fileName">.pfx file name</param>
        /// <param name="pwd"> password for .pfx file</param>
        /// <returns>The requested certificate. If no valid certificate is found, returns null.</returns>
		public static X509Certificate2 GetCertificateFromFile(string fileName, SecureString pwd)
        {
            X509Certificate2 certificate = null;


            return certificate;
        }
    }
}