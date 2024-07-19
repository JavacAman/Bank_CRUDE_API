using System.ComponentModel.DataAnnotations;

namespace crude_class_library
{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }


        public string DepartmentAlias { get; set; }

        public string AdminEmail { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
