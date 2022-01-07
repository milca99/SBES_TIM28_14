using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp
{
    public class Database
    {
        public static List<string> GetComplaints ()
        {
            try
            {
                List<string> complaints = new List<string>();

                string fileName = @"complaints.txt";

                foreach (string line in File.ReadLines(fileName, Encoding.UTF8))
                {
                    complaints.Add(line);
                }

                return complaints;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
