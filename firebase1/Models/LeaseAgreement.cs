using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace firebase1.Models
{
    public class LeaseAgreement
    {

        [Key]
        public string LeaseAgreementId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Enter your Student Number:")]
        public string StudentNumber { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^(?!\.)(""([^""\r\\]\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_'{|}~]|(?<!\.)\.)*)(?<!)")]
        [StringLength(70)]
        [Display(Name = "Enter Your Email:")]

        public string Email { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Enter your ID:")]
        public string StudentID { get; set; }




        //landlord details

        [Display(Name = "Enter your Landlord's full names:")]
        [Required(ErrorMessage = "Landlord details are required")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Your fullname may not be less than 2 characters.")]
        public string LandLord { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Enter your Landlord's ID:")]
        public string LandlordId { get; set; }

        [Display(Name = "Enter your accomodation name:")]
        [Required(ErrorMessage = "Accomodation details are required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Your fullname may not be less than 2 characters.")]
        public string Accomodation { get; set; }


        //The thing from the past data

        //[Required(ErrorMessage = "Message is required:")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Number")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Response is required:")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Response")]
        public string ResponseMessage { get; set; }







    }
}