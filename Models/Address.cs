using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Display(Name ="House/Flat No")]
        public string houseNo { get; set; }
        [Display(Name ="Street")]
        public string street { get; set; }
        [Display(Name ="City")]
        public string city { get; set; }
        [Display(Name ="State")]
        public string state { get; set; }
        [Display(Name ="Country")]
        public string country { get; set; }

        [ForeignKey("Account")]
        public int? accountId { get; set; }
        public Account? account { get; set; }
    }
}
