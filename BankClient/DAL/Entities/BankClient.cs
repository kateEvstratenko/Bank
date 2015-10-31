using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BankClient: IBaseEntity
    {
        [Key, ForeignKey("Customer")]
        public int Id { get; set; }
        public string Phone { get; set; }
        public virtual Customer Customer { get; set; }
    }
}