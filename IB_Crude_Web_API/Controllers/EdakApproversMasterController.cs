
using crude_class_library;
using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdakApproverMasterController : ControllerBase
    {
        private readonly IBDatabaseDbContext _context;
        private readonly TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public EdakApproverMasterController(IBDatabaseDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var edakapprovers = _context.EdakApproversMaster.Find(id);

                if (edakapprovers == null)
                {
                    return NotFound("Department Admin not found");
                }

                return Ok(edakapprovers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Department Admin: " + ex.Message);
            }
        }

        //Create
        [HttpPost]
        [Route("Create")]
        public Models.EdakApproversMaster Create(Models.EdakApproversMaster edakapprovers)
        {
            try
            {
                crude_class_library.Departments objedk = _context.Departments.Where(t => t.DepartmentName == edakapprovers.departmentName).FirstOrDefault();
                if (objedk != null)
                {
                    crude_class_library.EdakApproversMaster newDepartmentAdmin = new crude_class_library.EdakApproversMaster();
                    //var newDepartmentAdmin = new DepartmentAdmin();
                    newDepartmentAdmin.DepartmentId = objedk.DepartmentId;
                    newDepartmentAdmin.ApproverType = edakapprovers.ApproverType;
                    newDepartmentAdmin.ApproverEmail = edakapprovers.ApproverEmail;
                    newDepartmentAdmin.CreatedBy = edakapprovers.CreatedBy;
                    newDepartmentAdmin.ModifiedBy = edakapprovers.ModifiedBy;
                    newDepartmentAdmin.SecretaryEmail = edakapprovers.SecretaryEmail;
                    newDepartmentAdmin.ApproverOrder = edakapprovers.ApproverOrder;
                    newDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                    newDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                    _context.EdakApproversMaster.Add(newDepartmentAdmin);
                    _context.SaveChanges();


                }
                return edakapprovers;

            }
            catch (Exception ex)
            {
                return edakapprovers;
            }
        }

        //Edit
        [HttpPost]
        [Route("Edit")]
        public Models.EdakApproversMaster Edit(Models.EdakApproversMaster edakapprovers)
        {
            try
            {
                if (edakapprovers.EdakApproverMasterId != null)
                {
                    var existingDepartmentAdmin = _context.EdakApproversMaster
                        .Where(d => d.EdakApproverMasterId == edakapprovers.EdakApproverMasterId)
                        .FirstOrDefault();

                    if (existingDepartmentAdmin != null)
                    {

                        existingDepartmentAdmin.DepartmentId = edakapprovers.DepartmentId;
                        existingDepartmentAdmin.ApproverType = edakapprovers.ApproverType;
                        existingDepartmentAdmin.ApproverEmail = edakapprovers.ApproverEmail;
                        existingDepartmentAdmin.SecretaryEmail = edakapprovers.SecretaryEmail;
                        existingDepartmentAdmin.ApproverOrder = edakapprovers.ApproverOrder;
                        existingDepartmentAdmin.CreatedBy = edakapprovers.CreatedBy;
                        existingDepartmentAdmin.ModifiedBy = edakapprovers.ModifiedBy;
                        existingDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        existingDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                        _context.EdakApproversMaster.Update(existingDepartmentAdmin);
                        _context.SaveChanges();
                    }
                }

                return edakapprovers;
            }
            catch (Exception ex)
            {
                return edakapprovers;
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var edakapprovers = _context.EdakApproversMaster.Find(id);

                if (edakapprovers == null)
                {
                    return NotFound("Department Admin not found");
                }

                _context.EdakApproversMaster.Remove(edakapprovers);
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