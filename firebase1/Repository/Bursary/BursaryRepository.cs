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

namespace firebase1.Repository.Bursary
{
    public class BursaryRepository : IBursary, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public BursaryRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddBursary(Models.Bursary bursary)
        {
            var bursaryData = bursary;
            PushResponse pushResponse = _firebaseClient.Push("Bursary/", bursaryData);
            bursaryData.BursaryId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Bursary/" + bursaryData.BursaryId, bursaryData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task EditBursary(Models.Bursary bursary)
        {
            string body = string.Empty;
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new System.IO.StreamReader(root + @"/EmailTemplate/EmailTemplate.txt")) 
            {
               string readFile = reader.ReadToEnd();
               string strContent = string.Empty;
               strContent =  readFile;
               strContent = strContent.Replace("[Fullname]", bursary.FullName);
               strContent = strContent.Replace("[RespondMessage]", bursary.FullName);
               body = strContent.ToString();
            }

            MailMessage mailMessage = new MailMessage("EduConnect@gmail.com", "EduConnect@gmail.com");
            mailMessage.Subject = "Ref Num: " + bursary.BursaryId + "-" + "EduConnect Response.";
            mailMessage.Body = body;

            await Mail.SendMail(mailMessage);

            SetResponse firebaseSetResponse = _firebaseClient.Set("Bursary/" + bursary.BursaryId, bursary);
        }

        public List<Models.Bursary> GetAllBursary()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Bursary");
            dynamic bursaryData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var bursaryList = new List<Models.Bursary>();
            if (bursaryData != null)
            {
                foreach (var bursary in bursaryData)
                {
                    bursaryList.Add(JsonConvert.DeserializeObject<Models.Bursary>(((JProperty)bursary).Value.ToString()));


                }
            }
            return bursaryList;
        }

        public void RemoveBursary(string bursaryId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Bursary/" + bursaryId);
        }

        public Models.Bursary ShowBursary(string bursaryId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Bursary/" + bursaryId);
            Models.Bursary bursary = JsonConvert.DeserializeObject<Models.Bursary>(firebaseResponse.Body);
            return bursary;
        }
    }
}