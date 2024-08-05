using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebase1.Repository.Comment
{
    public interface IComment
    {
        void AddComment(firebase1.Models.Comment comment);
        void RemoveComment(string commentId);

        firebase1.Models.Comment ShowComment(string commentId);
        List<Models.Comment> GetAllComment();
        //Use with sendmail function (asychronous coding)
        Task EditComment(firebase1.Models.Comment comment);
    }
}
