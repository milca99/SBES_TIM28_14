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


    }
}
