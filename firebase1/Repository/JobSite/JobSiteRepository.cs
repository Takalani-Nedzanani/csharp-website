using firebase1.Repository.DataConnection;
using firebase1.Repository.Utilities;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace firebase1.Repository.JobSite
{
    public class JobSiteRepository : IJobSite, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public JobSiteRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddJobSite(Models.JobSite jobSite)
        {
            var jobSiteData = jobSite;
            PushResponse pushResponse = _firebaseClient.Push("JobSite/", jobSiteData);
            jobSiteData.JobSiteId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("JobSite/" + jobSiteData.JobSiteId, jobSiteData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task EditJobSite(Models.JobSite jobSite)
        {
            string body = string.Empty;
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new System.IO.StreamReader(root + @"/EmailTemplate/EmailTemplate.txt")) 
            {
               string readFile = reader.ReadToEnd();
               string strContent = string.Empty;
               strContent =  readFile;
               strContent = strContent.Replace("[Fullname]", jobSite.FullName);
               strContent = strContent.Replace("[RespondMessage]", jobSite.FullName);
               body = strContent.ToString();
            }

            MailMessage mailMessage = new MailMessage("EduConnect@gmail.com", "EduConnect@gmail.com");
            mailMessage.Subject = "Ref Num: " + jobSite.JobSiteId + "-" + "EduConnect Response.";
            mailMessage.Body = body;

            await Mail.SendMail(mailMessage);

            SetResponse firebaseSetResponse = _firebaseClient.Set("JobSite/" + jobSite.JobSiteId, jobSite);
        }

        public List<Models.JobSite> GetAllJobSite()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("JobSite");
            dynamic jobSiteData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var jobSiteList = new List<Models.JobSite>();
            if (jobSiteData != null)
            {
                foreach (var jobSite in jobSiteData)
                {
                    jobSiteList.Add(JsonConvert.DeserializeObject<Models.JobSite>(((JProperty)jobSite).Value.ToString()));


                }
            }
            return jobSiteList;
        }

        public void RemoveJobSite(string jobSiteId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("JobSite/" + jobSiteId);
        }

        public Models.JobSite ShowJobSite(string jobSiteId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("JobSite/" + jobSiteId);
            Models.JobSite jobSite = JsonConvert.DeserializeObject<Models.JobSite>(firebaseResponse.Body);
            return jobSite;
        }
    }
}