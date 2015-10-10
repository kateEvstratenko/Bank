using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BankClient
    {
        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string Phone { get; set; }
        public string MilitaryIdPath { get; set; }
        public string IncomeCertificatePath { get; set; }
        public virtual Customer Customer { get; set; }
    }
}