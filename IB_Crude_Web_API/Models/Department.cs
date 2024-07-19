using System.ComponentModel.DataAnnotations;

namespace IB_Crude_Web_API.Models
{
    public class Department
    {
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
