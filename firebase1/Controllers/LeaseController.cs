using firebase1.Models;
using firebase1.Repository.Lease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace firebase1.Controllers
{
    public class LeaseController : Controller
    {
       private LeaseRepository _leaseRepository;

        public LeaseController() 
        {
            _leaseRepository = new LeaseRepository();
        } 


        // GET: Lease
        public ActionResult Index()
        {
             return View(_leaseRepository.GetAllLease());
        }


        // Get:Lease
        public ActionResult Create()
        {
            return View();
        }
      

    [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(LeaseAgreement leaseAgreement)
        {
            try
            {
                if (string.IsNullOrEmpty(leaseAgreement.Message) == false)
                {
                    _leaseRepository.AddLease(leaseAgreement);
                    ModelState.AddModelError(string.Empty, "Your Lease Details has been sent, we will be in contact soon.");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, " No Empty Spaces my be left, then click the send.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Your Lease Details were not sent. Kindly view the Lease information.");
            }
            return View();
        }

        //Details
        [HttpGet]

        public ActionResult Details(string id)
        {
            return View(_leaseRepository.ShowLease(id));
        }

      
       //Delete
        [HttpGet]

        public ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id) == false)
            {
                _leaseRepository.RemoveLease(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Lease Details cannot be found"); 
                return View();    
            }
            return RedirectToAction("Index", "Lease");
        }


       [HttpGet]
        public ActionResult Edit( string id){
          return View(_leaseRepository.ShowLease(id));}


        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(LeaseAgreement leaseAgreement)
        {
            try
            {
                await _leaseRepository.EditLease(leaseAgreement);
                ModelState.AddModelError(string.Empty, "Lease Response successful.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Lease Response not successful.");
            }
            return RedirectToAction("Index", "Lease");



        }


    }
}