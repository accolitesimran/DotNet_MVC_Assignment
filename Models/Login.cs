using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Enter an email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage ="Enter a password")]
        public string password { get; set; }
       
    }
}
