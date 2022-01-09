using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Contracts
{
    public class XMLHelper
    {
        public static List<string> ReadXml(string file)
        {
            string xmlFile = File.ReadAllText(file);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlFile);
            List<string> users = new List<string>();
            XmlNodeList idNodes = xmldoc.SelectNodes("user/name");
            foreach (XmlNode node in idNodes)
                users.Add(node.InnerText);
            return users;
        }
        public static void WriteXML(string username)
        {
            // Checks if user is already banned
            string fileName = @"banned_certs.xml";
            var bannedUsers = ReadXml(fileName);

            if (bannedUsers.Contains(username))
            {
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();
            string name = @"banned_certs.xml";
            xmlDocument.Load(name);
            XmlElement childElement = xmlDocument.CreateElement("name");
            childElement.InnerText = username;
            XmlNode parentNode = xmlDocument.SelectSingleNode("user");
            parentNode.InsertBefore(childElement, parentNode.FirstChild);
            xmlDocument.Save(name);
        }

      public static void RemoveUser(string user)
        {
            XmlDocument doc = new XmlDocument();
            string filename = @"banned_certs.xml";
            string xmlFile = File.ReadAllText(filename);
            doc.Load(filename);


            XmlNodeList idNodes = doc.SelectNodes("user/name");
            foreach (XmlNode node in idNodes)
            {
                if (node.InnerText.Equals(user))
                {
                    node.ParentNode.RemoveChild(node);

                }
            }
            doc.Save(filename);
        }

    }
}
