using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase1.Repository.JobSite
{
    public interface IJobSite
    {
        void AddJobSite(firebase1.Models.JobSite jobSite);
        void RemoveJobSite(string jobSiteId);

        firebase1.Models.JobSite ShowJobSite(string jobSite);
        List<Models.JobSite> GetAllJobSite();
        //Use with sendmail function (asychronous coding)
        Task EditJobSite(firebase1.Models.JobSite jobSite);
    }
}
