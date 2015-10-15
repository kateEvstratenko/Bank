using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{
   public class RegistreUserAddressViewModel
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string House { get; set; }
        public int? Flat { get; set; }
    }
}
