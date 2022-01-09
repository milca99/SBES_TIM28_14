using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace SecurityManager
{
    public enum AuditEventTypes
    {
        BanTheUserSuccess = 0,
        ForgiveSuccess = 1,
        SendComplaintSuccess = 2
    }

    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager
                            (typeof(AuditEventFile).ToString(),
                            Assembly.GetExecutingAssembly());
                    }
                    return resourceManager;
                }
            }
        }

        public static string BanTheUserSuccess
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.BanTheUserSuccess.ToString());
            }
        }
        public static string ForgiveSuccess
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.ForgiveSuccess.ToString());
            }
        }
        public static string SendComplaintSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.SendComplaintSuccess.ToString());
            }
        }


    }
}
