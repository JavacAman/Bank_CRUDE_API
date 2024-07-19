using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
            IBDatabaseDbContext _context;
            TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            public DepartmentController(IBDatabaseDbContext context)
            {
                _context = context;
            }
        // GET

            [HttpGet("{id}")]
            public ActionResult Get(int id)
            {
                try
                {
                    var department = _context.Departments.Find(id);

                    if (department == null)
                    {
                        return NotFound("Department not found");
                    }

                    return Ok(department);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving department: " + ex.Message);
                }
            }

//Create
            [HttpPost]
            [Route("Create")]
            public Models.Departments Create(Models.Departments department)
            {
                try
                {
                    var newDepartment = new crude_class_library.Departments();
                    newDepartment.DepartmentName = department.DepartmentName;
                    newDepartment.CreatedBy = department.CreatedBy;
                    newDepartment.ModifiedBy = department.ModifiedBy;
                    newDepartment.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                    newDepartment.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                    _context.Departments.Add(newDepartment);
                    _context.SaveChanges();

                    return department;
                }
                catch (Exception ex)
                {
                    return department;
                }
            }

        //Edit
            [HttpPost]
            [Route("Edit")]
            public Models.Departments Edit(Models.Departments department)
            {
                try
                {
                    if (department.DepartmentId != null)
                    {
                        var existingDepartment = _context.Departments
                            .Where(d => d.DepartmentId == department.DepartmentId)
                            .FirstOrDefault();

                        if (existingDepartment != null)
                        {
                            existingDepartment.DepartmentName = department.DepartmentName;
                            existingDepartment.CreatedBy = department.CreatedBy;
                            existingDepartment.ModifiedBy = department.ModifiedBy;
                            existingDepartment.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                            existingDepartment.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                            _context.Departments.Update(existingDepartment);
                            _context.SaveChanges();
                        }
                    }

                    return department;
                }
                catch (Exception ex)
                {
                    return department;
                }
            }


            [HttpDelete("{id}")]
            public ActionResult Delete(int id)
            {
                try
                {
                    var department = _context.Departments.Find(id);

                    if (department == null)
                    {
                        return NotFound("Department not found");
                    }

                    _context.Departments.Remove(department);
                    _context.SaveChanges();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting department: " + ex.Message);
                }
            }

        }
    }
