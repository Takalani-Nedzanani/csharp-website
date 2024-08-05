using firebase1.Models;
using firebase1.Repository.JobSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace firebase1.Controllers
{
    public class JobSitesController : Controller
    {
        private JobSiteRepository _jobSiteRepository;

        public JobSitesController() 
        {
            _jobSiteRepository = new JobSiteRepository();
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(_jobSiteRepository.GetAllJobSite());
        }

       
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        // GET: Contact

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(JobSite jobSite)
        {
            try
            {
                if(string.IsNullOrEmpty(jobSite.Message) == false)
                {
                    _jobSiteRepository.AddJobSite(jobSite);
                    ModelState.AddModelError(string.Empty, "Job Site has been  added successfully ");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly enter a job Site , then click the send JobSite.");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, "Your JobSite  was not sent. Kindly view the JobSite information.");
            }
            return View();
        }


        //Details
        [HttpGet]

        public ActionResult Details(string id)
        {
            return View(_jobSiteRepository.ShowJobSite(id));
        }

        //Delete
        [HttpGet]

        public ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id) == false)
            {
                _jobSiteRepository.RemoveJobSite(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "jobsite  cannot be found"); 
                return View();    
            }
            return RedirectToAction("Index", "JobSite");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(_jobSiteRepository.ShowJobSite(id));
            
         }

        [HttpPost]
        [ValidateInput(false)]
        public async Task <ActionResult> Edit(JobSite jobSite)
        {
            try 
            {
                await _jobSiteRepository.EditJobSite(jobSite);
                ModelState.AddModelError(string.Empty, "Contact Response successful.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Contact Response not successful.");
            }
           
            return RedirectToAction("Index", "JobSite");

        }

    }
} 