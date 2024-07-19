using System.ComponentModel.DataAnnotations.Schema;

namespace IB_Crude_Web_API.Models
{
    public class BoardCommittees
    {
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
