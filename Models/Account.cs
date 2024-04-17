using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [MinLength(3)]
        [MaxLength(100)]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [MinLength(3)]
        [MaxLength(100)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage ="Please enter a password")]
        [StringLength(1000, MinimumLength = 6)]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        [Required(ErrorMessage ="Please enter the password")]
        public string confirmPassword { get; set; }


        [ScaffoldColumn(false)]
        public Address? address { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Mobile no not valid")]
        [ScaffoldColumn(false)]
        public string? phoneNo { get; set; }

        [ScaffoldColumn(false)]
        public List<Url>? profiles { get; set; }
    }
}
