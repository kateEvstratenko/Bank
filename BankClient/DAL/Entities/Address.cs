using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Address: IBaseEntity
    {
        [Key, ForeignKey("Customer")]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Flat { get; set; }
        public virtual Customer Customer { get; set; }
    }
}