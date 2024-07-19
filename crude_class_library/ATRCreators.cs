using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crude_class_library
{
    public class ATRCreators
    {
        [Key]
        public int? ATRCreatorId { get; set; }

        public string? ATRCreatorEmail { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
