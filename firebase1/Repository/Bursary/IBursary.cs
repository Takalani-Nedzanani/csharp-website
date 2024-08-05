using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase1.Repository.Bursary
{
    public interface IBursary
    {
        void AddBursary(firebase1.Models.Bursary bursary);
        void RemoveBursary(string bursaryId);

        firebase1.Models.Bursary ShowBursary(string bursaryId);
        List<Models.Bursary> GetAllBursary();
        //Use with sendmail function (asychronous coding)
        Task EditBursary(firebase1.Models.Bursary bursary);
    }
}
