using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase1.Repository.Communiques
{
    public interface ICommunique
    {
        void AddCommuniques(firebase1.Models.Communiques communiques);
        void RemoveCommuniques(string communiqueId);

        firebase1.Models.Communiques ShowCommuniques(string communiqueId);
        List<Models.Communiques> GetAllCommuniques();
        //Use with sendmail function (asychronous coding)
        Task EditCommuniques(firebase1.Models.Communiques communiques);
    }
}
