using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace firebase1.Models
{
    public class SignUp
    {
        //[Key]
        //public int Id { get; set; }
        //[Required]
        //[Display(Name = "Full Name and Surname")]
        //public string Name { get; set; }
        //[Required,Display(Name = "Email Address")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        ////[Required, Display(Name = "Password")]
        ////public string Password { get; set; }

        //[Required(ErrorMessage = " Password is required")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password {  get; set; }


        //[Compare("Password",ErrorMessage ="Please confirm your password.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        //public string ConfirmPassword { get; set;}

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; }
        [Required, Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Display(Name = "Password")]
        public string Password { get; set; }



    }
}