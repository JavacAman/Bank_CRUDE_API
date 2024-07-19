using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crude_class_library
{
    public class NoteApproversMaster
    {
        [Key]
        public int NoteApproverMasterId { get; set; }
        public int DepartmentId { get; set; }
        public int ApproverType { get; set; }
        public string ApproverEmail { get; set; }
        public string SecretaryEmail { get; set; }
        public int ApproverOrder { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Designation { get; set; }

    }
}
