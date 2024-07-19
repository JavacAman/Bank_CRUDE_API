using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IB_Crude_Web_API.Models
{
    public class EdakApproversMaster
    {
        [Key]
        public int EdakApproverMasterId { get; set; }
        public int DepartmentId { get; set; }
        public string departmentName { get; set; }

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
    }
}
