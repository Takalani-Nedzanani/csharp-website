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

namespace firebase1.Repository.Comment
{
    public class CommentRepository : IComment, IDisposable
    {
        FirebaseConnect connect = new FirebaseConnect();
        private IFirebaseClient _firebaseClient;

        public CommentRepository()
        {
            _firebaseClient = connect.firebaseClient;
        }



        public void AddComment(Models.Comment comment)
        {
            var commentData = comment;
            PushResponse pushResponse = _firebaseClient.Push("Comment/", commentData);
            commentData.CommentId = pushResponse.Result.name;
            SetResponse setResponse = _firebaseClient.Set("Comment/" + commentData.CommentId, commentData);

        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task EditComment(Models.Comment comment)
        {
            string body = string.Empty;
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new System.IO.StreamReader(root + @"/EmailTemplate/EmailTemplate.txt")) 
            {
               string readFile = reader.ReadToEnd();
               string strContent = string.Empty;
               strContent =  readFile;
               strContent = strContent.Replace("[Fullname]", comment.FullName);
               strContent = strContent.Replace("[RespondMessage]", comment.FullName);
               body = strContent.ToString();
            }

            MailMessage mailMessage = new MailMessage("educonnecta11@gmail.com", "educonnecta11@gmail.com");
            mailMessage.Subject = "Ref Num: " + comment.CommentId + "-" + "EduConnect Response.";
            mailMessage.Body = body;

            await Mail.SendMail(mailMessage);

            SetResponse firebaseSetResponse = _firebaseClient.Set("Comment/" + comment.CommentId, comment);
        }

        public List<Models.Comment> GetAllComment()
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Comment");
            dynamic commentData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            var commentList = new List<Models.Comment>();
            if (commentData != null)
            {
                foreach (var comment in commentData)
                {
                    commentList.Add(JsonConvert.DeserializeObject<Models.Comment>(((JProperty)comment).Value.ToString()));


                }
            }
            return commentList;
        }

        public void RemoveComment(string commentId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Delete("Comment/" + commentId);
        }

        public Models.Comment ShowComment(string commentId)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("Comment/" + commentId);
            Models.Comment comment = JsonConvert.DeserializeObject<Models.Comment>(firebaseResponse.Body);
            return comment;
        }
    }
}