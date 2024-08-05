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

namespace firebase1.Repository.Communiques
{
    public class CommuniqueRepository : ICommunique, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public CommuniqueRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }
        public void AddCommuniques(Models.Communiques communiques)
        {
            var communiquesData = communiques;
            PushResponse pushResponse = _firebaseClient.Push("Communiques/", communiquesData);
            communiquesData.CommuniqueId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Communiques/" + communiquesData.CommuniqueId, communiquesData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task EditCommuniques(Models.Communiques communiques)
        {
            string body = string.Empty;
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new System.IO.StreamReader(root + @"/EmailTemplate/EmailTemplate.txt")) 
            {
               string readFile = reader.ReadToEnd();
               string strContent = string.Empty;
               strContent =  readFile;
               strContent = strContent.Replace("[Fullname]", communiques.FullName);
               strContent = strContent.Replace("[RespondMessage]", communiques.FullName);
               body = strContent.ToString();
            }

            MailMessage mailMessage = new MailMessage("educonnecta11@gmail.com", "educonnecta11@gmail.com");
            mailMessage.Subject = "Ref Num: " + communiques.CommuniqueId + "-" + "EduConnect Response.";
            mailMessage.Body = body;

            await Mail.SendMail(mailMessage);

            SetResponse firebaseSetResponse = _firebaseClient.Set("Communiques/" + communiques.CommuniqueId, communiques);
        }

        public List<Models.Communiques> GetAllCommuniques()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Communiques");
            dynamic communiqueData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var communiqueList = new List<Models.Communiques>();
            if (communiqueData != null)
            {
                foreach (var communiques in communiqueData)
                {
                    communiqueList.Add(JsonConvert.DeserializeObject<Models.Communiques>(((JProperty)communiques).Value.ToString()));


                }
            }
            return communiqueList;
        }

        public void RemoveCommuniques(string communiqueId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Communiques/" + communiqueId);
        }

        public Models.Communiques ShowCommuniques(string communiqueId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Communiques/" + communiqueId);
            Models.Communiques communique = JsonConvert.DeserializeObject<Models.Communiques>(firebaseResponse.Body);
            return communique;
        }
    }
}