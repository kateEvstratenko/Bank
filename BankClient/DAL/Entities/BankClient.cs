using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BankClient
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Phone { get; set; }
        public string MilitaryIdPath { get; set; }
        public string IncomeCertificatePath { get; set; }
        public virtual Person Person { get; set; }
    }
}