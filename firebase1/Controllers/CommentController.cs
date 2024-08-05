using firebase1.Models;
using firebase1.Repository.Comment;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace firebase1.Controllers
{
    public class CommentController : Controller
    {
        private CommentRepository _commentRepository;

        public CommentController() 
        {
            _commentRepository = new CommentRepository();
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(_commentRepository.GetAllComment());
        }

       
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        // GET: Contact

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(Comment comment)
        {
            try
            {
                if(string.IsNullOrEmpty(comment.Message) == false)
                {
                    _commentRepository.AddComment(comment);
                    ModelState.AddModelError(string.Empty, "Your comment  has been sent.");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly enter a comment, then click the send comment.");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, "Your comment was not sent. Kindly view the comment information.");
            }
            return View();
        }


        //Details
        [HttpGet]

        public ActionResult Details(string id)
        {
            return View(_commentRepository.ShowComment(id));
        }

        //Delete
        [HttpGet]

        public ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id) == false)
            {
                _commentRepository.RemoveComment(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Comment  cannot be found"); 
                return View();    
            }
            return RedirectToAction("Index", "Comment");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(_commentRepository.ShowComment(id));
            
         }

        [HttpPost]
        [ValidateInput(false)]
        public async Task <ActionResult> Edit(Comment comment)
        {
            try 
            {
                await _commentRepository.EditComment(comment);
                ModelState.AddModelError(string.Empty, "Comment Response successful.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Comment Response not successful.");
            }
           
            return RedirectToAction("Index", "Comment");

        }

    }
} 