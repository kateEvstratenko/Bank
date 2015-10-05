using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Address
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string House { get; set; }
        public int Flat { get; set; }
        public virtual Person Person { get; set; }
    }
}