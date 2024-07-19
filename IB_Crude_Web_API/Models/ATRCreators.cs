using System.ComponentModel.DataAnnotations.Schema;

namespace IB_Crude_Web_API.Models
{
    public class ATRCreators
    {
        public int ATRCreatorId { get; set; }

        public string ATRCreatorEmail { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
