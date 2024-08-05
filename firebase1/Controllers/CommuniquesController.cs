using firebase1.Models;
using firebase1.Repository.Communiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace firebase1.Controllers
{
    public class CommuniquesController : Controller
    {
        private CommuniqueRepository _communiqueRepository;

        public CommuniquesController() 
        {
            _communiqueRepository = new CommuniqueRepository();
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(_communiqueRepository.GetAllCommuniques());
        }

       
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        // GET: Contact

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(Communiques communiques)
        {
            try
            {
                if(string.IsNullOrEmpty(communiques.Message) == false)
                {
                    _communiqueRepository.AddCommuniques(communiques);
                    ModelState.AddModelError(string.Empty, "Your Communique has been Sent succefully");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly enter a communique, then click the send communique.");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, "Your communique was not sent. Kindly view the communique information.");
            }
            return View();
        }


        //Details
        [HttpGet]

        public ActionResult Details(string id)
        {
            return View(_communiqueRepository.ShowCommuniques(id));
        }

        //Delete
        [HttpGet]

        public ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id) == false)
            {
                _communiqueRepository.RemoveCommuniques(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "communique Message cannot be found"); 
                return View();    
            }
            return RedirectToAction("Index", "Communiques");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(_communiqueRepository.ShowCommuniques(id));
            
         }

        [HttpPost]
        [ValidateInput(false)]
        public async Task <ActionResult> Edit(Communiques communiques)
        {
            try 
            {
                await _communiqueRepository.EditCommuniques(communiques);
                ModelState.AddModelError(string.Empty, "communique Response successful.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "communique Response not successful.");
            }
           
            return RedirectToAction("Index", "Communiques");

        }

    }
} 