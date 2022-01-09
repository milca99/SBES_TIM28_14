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
        [FaultContract(typeof(SecurityException))]
        void TestCommunication();

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void SendComplaint(string user, string complaint, byte[] sign);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        List<string> ListComplaintsWithBannedWords();

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void BanTheUser(string username);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void Forgive(string user);

    }
}
