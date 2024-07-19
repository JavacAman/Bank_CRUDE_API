using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAdminController : ControllerBase
    {
        private readonly IBDatabaseDbContext _context;
        private readonly TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public DepartmentAdminController(IBDatabaseDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var departmentAdmin = _context.DepartmentAdmin.Find(id);

                if (departmentAdmin == null)
                {
                    return NotFound("Department Admin not found");
                }

                return Ok(departmentAdmin);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Department Admin: " + ex.Message);
            }
        }

        //Create
        [HttpPost]
        [Route("Create")]
        public  Models.DepartmentAdmin Create(Models.DepartmentAdmin departmentAdmin)
        {
            try
            {
                if (departmentAdmin != null)
                {
                    crude_class_library.Departments objDep = _context.Departments.Where(t => t.DepartmentName == departmentAdmin.departmentName).FirstOrDefault();
                    if (objDep != null)
                    {
                        crude_class_library.DepartmentAdmin newDepartmentAdmin = new crude_class_library.DepartmentAdmin();
                        //var newDepartmentAdmin = new DepartmentAdmin();
                        newDepartmentAdmin.DepartmentId = objDep.DepartmentId;
                        newDepartmentAdmin.Active = departmentAdmin.Active;
                        newDepartmentAdmin.Deleted = departmentAdmin.Deleted;
                        newDepartmentAdmin.CreatedBy = departmentAdmin.CreatedBy;
                        newDepartmentAdmin.ModifiedBy = departmentAdmin.ModifiedBy;
                        newDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        newDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                        _context.DepartmentAdmin.Add(newDepartmentAdmin);
                        _context.SaveChanges();


                    }
                   
                }
                
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("An error occurred: " + ex.Message);

                // Throw an exception with an appropriate status code and message
                throw new ApplicationException("Error creating Department Admin: " + ex.Message);
                return departmentAdmin;
            }
            return departmentAdmin;
        }

        //Edit
        [HttpPost]
        [Route("Edit")]
        public Models.DepartmentAdmin Edit(Models.DepartmentAdmin departmentAdmin)
        {
            try
            {
                if (departmentAdmin.DeptAdminId != null)
                {
                    crude_class_library.DepartmentAdmin existingDepartmentAdmin = _context.DepartmentAdmin.Where(d => d.DeptAdminId == departmentAdmin.DeptAdminId).FirstOrDefault();

                    if (existingDepartmentAdmin != null)
                    {

                        existingDepartmentAdmin.DepartmentId = departmentAdmin.DepartmentId;
                        existingDepartmentAdmin.Active = departmentAdmin.Active;
                        existingDepartmentAdmin.Deleted = departmentAdmin.Deleted;
                        existingDepartmentAdmin.CreatedBy = departmentAdmin.CreatedBy;
                        existingDepartmentAdmin.ModifiedBy = departmentAdmin.ModifiedBy;
                        existingDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        existingDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                        _context.DepartmentAdmin.Update(existingDepartmentAdmin);
                        _context.SaveChanges();
                    }
                }

                return departmentAdmin;
            }
            catch (Exception ex)
            {
                return departmentAdmin;

            }
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var departmentAdmin = _context.DepartmentAdmin.Find(id);

                if (departmentAdmin == null)
                {
                    return NotFound("Department Admin not found");
                }

                _context.DepartmentAdmin.Remove(departmentAdmin);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Department Admin: " + ex.Message);
            }
        }
    }
}
