using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This is required")]
        [Display(Name = "Platform Name")]
        public string name { get; set; }

        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Link to the profile is mandatory")]
        [Display(Name = "Link")]
        public string url { get; set; }

        [ForeignKey("Account")]
        public int? accountId { get; set; }
        public Account? account { get; set; }

    }
}
