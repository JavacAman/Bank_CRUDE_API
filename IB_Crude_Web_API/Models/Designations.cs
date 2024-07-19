using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IB_Crude_Web_API.Models
{
    public class Designations
    {
        [Key]
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
