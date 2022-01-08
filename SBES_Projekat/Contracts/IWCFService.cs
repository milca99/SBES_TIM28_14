using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        void TestCommunication();

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void SendComplaint(string user, string complaint);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        List<string> ListComplaintsWithBannedWords();
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void BanTheUser(string username);
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void Forgive();

    }
}
