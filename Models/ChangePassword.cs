using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ChangePassword
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Old Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter your old password")]
        public string oldPassword { get; set; }
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter a new password")]
        public string newPassword { get; set; }
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter your password again")]
        [Compare("newPassword")]
        public string confirmNewPassword { get; set; }
    }
}
