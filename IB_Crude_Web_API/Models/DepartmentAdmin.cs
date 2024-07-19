using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IB_Crude_Web_API.Models
{
    public class DepartmentAdmin
    {
        public int DeptAdminId { get; set; }
        public int DepartmentId { get; set; }
        public string Active { get; set; }

        public bool Deleted { get; set; }


        public string CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string departmentName { get; set; }

    }
}

