using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crude_class_library
{
    public class BoardCommittees
    {
        [Key]
        public int BoardCommitteeId { get; set; }

        public string BoardCommitteeName { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }


        [Column(TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
