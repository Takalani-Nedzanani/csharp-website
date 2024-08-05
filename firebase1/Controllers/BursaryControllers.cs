using firebase1.Models;
using firebase1.Repository.Bursary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace firebase1.Controllers
{
    public class BursaryController : Controller
    {
        private BursaryRepository _bursaryRepository;

        public BursaryController() 
        {
            _bursaryRepository = new BursaryRepository();
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(_bursaryRepository.GetAllBursary());
        }

       
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        // GET: Contact

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(Bursary bursary)
        {
            try
            {
                if(string.IsNullOrEmpty(bursary.Message) == false)
                {
                    _bursaryRepository.AddBursary(bursary);
                    ModelState.AddModelError(string.Empty, "Bursary has been added succefully to the students.");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly enter a bursary, then click the send bursary.");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, "Your bursary was not sent. Kindly view the bursary information.");
            }
            return View();
        }


        //Details
        [HttpGet]

        public ActionResult Details(string id)
        {
            return View(_bursaryRepository.ShowBursary(id));
        }

        //Delete
        [HttpGet]

        public ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id) == false)
            {
                _bursaryRepository.RemoveBursary(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "bursary cannot be found"); 
                return View();    
            }
            return RedirectToAction("Index", "Bursary");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(_bursaryRepository.ShowBursary(id));
            
         }

        [HttpPost]
        [ValidateInput(false)]
        public async Task <ActionResult> Edit(Bursary bursary)
        {
            try 
            {
                await _bursaryRepository.EditBursary(bursary);
                ModelState.AddModelError(string.Empty, "Contact Response successful.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Contact Response not successful.");
            }
           
            return RedirectToAction("Index", "Bursary");

        }

    }
} 