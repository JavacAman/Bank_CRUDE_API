using System.ComponentModel.DataAnnotations.Schema;

namespace IB_Crude_Web_API.Models
{
    public class Committee
    {
        public int CommitteeId { get; set; }

        public string CommitteeName { get; set; }

        public string CommitteeAlias { get; set; }

        public string departmentName { get; set; }


        public string Convener { get; set; }

        public int ConvenerDepartmentId { get; set; }

        public string Chairman { get; set; }


        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        // Navigation property
        public Department ConvenerDepartment { get; set; }
    }
}
