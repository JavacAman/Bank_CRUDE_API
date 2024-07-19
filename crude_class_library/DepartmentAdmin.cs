using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace crude_class_library
{
    public class DepartmentAdmin
    {
        [Key]
        public int DeptAdminId { get; set; }
        public int DepartmentId { get; set; }
        public string Active { get; set; }

        public bool Deleted { get; set; }


        public string CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

