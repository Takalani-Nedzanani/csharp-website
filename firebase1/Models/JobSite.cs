using System.ComponentModel.DataAnnotations;

namespace firebase1.Models
{
    public class JobSite
    {

        [Key]
        public string JobSiteId { get; set; }

        [Display(Name = "JobSite Name")]
        [Required(ErrorMessage = "JobSite Name is required")]
        [StringLength (50,MinimumLength =2,ErrorMessage ="Your fullname may not be less than 2 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^(?!\.)(""([^""\r\\]\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_'{|}~]|(?<!\.)\.)*)(?<!)")]
        [StringLength(70)]
        [Display(Name = "Your Email")]

        public string Email { get; set; }

        [Required(ErrorMessage ="JobSite link is required")]
        [DataType(DataType.Url)]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "JobSite Link")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Response is required")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Response")]
        public string ResponseMessage { get; set; }




    }
}