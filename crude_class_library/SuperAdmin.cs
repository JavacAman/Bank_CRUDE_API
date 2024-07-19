using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crude_class_library
{
    public class SuperAdmin
    {
        [Key]
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
