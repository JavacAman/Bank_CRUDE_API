using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crude_class_library
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
