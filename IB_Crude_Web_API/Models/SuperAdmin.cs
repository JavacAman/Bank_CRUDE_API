using System.ComponentModel.DataAnnotations.Schema;

namespace IB_Crude_Web_API.Models
{
    public class SuperAdmin
    {
        public int SuperAdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}
