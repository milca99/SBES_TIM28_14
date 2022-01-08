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
        void SendComplaint(string user, string complaint, byte[] sign);

        [OperationContract]
        List<string> ListComplaintsWithBannedWords();
        [OperationContract]
        void BanTheUser(string username);
        [OperationContract]
        void Forgive();

    }
}
