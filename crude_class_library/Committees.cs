using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crude_class_library
{
    public class Committees
    {
        [Key]
        public int CommitteeId { get; set; }

        public string CommitteeName { get; set; }

        public string CommitteeAlias { get; set; }


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
        public Departments ConvenerDepartment { get; set; }
    }
}
