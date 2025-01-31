﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace firebase1.Models
{
    public class Bursary
    {

        [Key]
        public string BursaryId { get; set; }

        [Display(Name = "Bursary Name")]
        [Required(ErrorMessage = "Fullname is required")]
        [StringLength (50,MinimumLength =2,ErrorMessage ="Your fullname may not be less than 2 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^(?!\.)(""([^""\r\\]\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_'{|}~]|(?<!\.)\.)*)(?<!)")]
        [StringLength(70)]
        [Display(Name = "Your Email")]

        public string Email { get; set; }

        [Required(ErrorMessage ="Message is required")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Bursary link")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Response is required")]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Response")]
        public string ResponseMessage { get; set; }




    }
}