using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SecurityManager
{
    public class Audit : IDisposable
    {

        private static EventLog customLog = null;
        const string SourceName = "Application"; 
        const string LogName = "Application"; 

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                customLog = new EventLog(LogName,
                    Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }


        public static void BanTheUserSuccess(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserBanSuccess =
                    AuditEvents.BanTheUserSuccess;
                string message = String.Format(UserBanSuccess,
                    userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.BanTheUserSuccess));
            }
        }
        public static void ForgiveSuccess(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserForgiveSuccess =
                    AuditEvents.ForgiveSuccess;
                string message = String.Format(UserForgiveSuccess,
                    userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ForgiveSuccess));
            }
        }

       


        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
