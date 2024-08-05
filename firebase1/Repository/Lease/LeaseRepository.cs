using firebase1.Repository.DataConnection;
using FireSharp.Interfaces;
using FireSharp.Response;
using firebase1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using firebase1.Repository.Utilities;


namespace firebase1.Repository.Lease
{
    public class LeaseRepository : ILease, IDisposable
    {
        //public void AddLease(LeaseAgreement leaseAgreement)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Dispose()
        //{
        //    this.Dispose();
        //}

        //public Task EditLease(LeaseAgreement leaseAgreement)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<LeaseAgreement> GetAllLease()
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveLease(string leaseAgreementId)
        //{
        //    throw new NotImplementedException();
        //}

        //public LeaseAgreement ShowLease(string leaseAgreementId)
        //{
        //    throw new NotImplementedException();
        //}


        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public LeaseRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddLease(Models.LeaseAgreement leaseAgreement)
        {
            var leaseAgreementData = leaseAgreement;
            PushResponse pushResponse = _firebaseClient.Push("Lease/", leaseAgreementData);
            leaseAgreementData.LeaseAgreementId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Lease/" + leaseAgreementData.LeaseAgreementId, leaseAgreementData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task EditLease(Models.LeaseAgreement leaseAgreement)
        {
            string body = string.Empty;
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new System.IO.StreamReader(root + @"/EmailTemplate/EmailTemplate.txt"))
            {
                string readFile = reader.ReadToEnd();
                string strContent = string.Empty;
                strContent = readFile;
                strContent = strContent.Replace("[Fullname]", leaseAgreement.StudentNumber);
                strContent = strContent.Replace("[RespondMessage]", leaseAgreement.ResponseMessage);
                body = strContent.ToString();
            }

            MailMessage mailMessage = new MailMessage("EduConnecta11@gmail.com", "EduConnecta11@gmail.com");
            mailMessage.Subject = "Ref Num: " + leaseAgreement.LeaseAgreementId + "-" + "EduConnect Response.";
            mailMessage.Body = body;

            await Mails.SendMail(mailMessage);

            SetResponse firebaseSetResponse = _firebaseClient.Set("Lease/" + leaseAgreement.LeaseAgreementId, leaseAgreement);
        }

        public List<Models.LeaseAgreement> GetAllLease()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Lease");
            dynamic leaseAgreementData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var leaseAgreementList = new List<Models.LeaseAgreement>();
            if (leaseAgreementData != null)
            {
                foreach (var leaseAgreement in leaseAgreementData)
                {
                    leaseAgreementList.Add(JsonConvert.DeserializeObject<Models.LeaseAgreement>(((JProperty)leaseAgreement).Value.ToString()));


                }
            }
            return leaseAgreementList;
        }

        public void RemoveLease(string leaseAgreementId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Lease/" + leaseAgreementId);
        }

        public Models.LeaseAgreement ShowLease(string leaseAgreementId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Lease/" + leaseAgreementId);
            Models.LeaseAgreement leaseAgreement = JsonConvert.DeserializeObject<Models.LeaseAgreement>(firebaseResponse.Body);
            return leaseAgreement;
        }





    }
}