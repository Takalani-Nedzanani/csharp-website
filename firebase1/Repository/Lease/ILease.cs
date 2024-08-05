using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase1.Repository.Lease
{
    internal interface ILease
    {
        void AddLease(firebase1.Models.LeaseAgreement leaseAgreement );
        void RemoveLease(string leaseAgreementId);

        firebase1.Models.LeaseAgreement ShowLease(string leaseAgreementId);
        List<Models.LeaseAgreement> GetAllLease();
        //Use with sendmail function (asychronous coding)
        Task EditLease(firebase1.Models.LeaseAgreement leaseAgreement);




    }
}
