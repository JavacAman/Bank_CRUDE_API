using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteApproverMasterController : ControllerBase
    {
        private readonly IBDatabaseDbContext _context;
        private readonly TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public NoteApproverMasterController(IBDatabaseDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var noteapprovers = _context.NoteApproversMaster.Find(id);

                if (noteapprovers == null)
                {
                    return NotFound("Approver not found");
                }

                return Ok(noteapprovers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Department Admin: " + ex.Message);
            }
        }

        //Create
        [HttpPost]
        [Route("Create")]
        public Models.NoteApproversMaster Create(Models.NoteApproversMaster noteapprovers)
        {
            try
            {
                crude_class_library.Departments objedk = _context.Departments.Where(t => t.DepartmentName == noteapprovers.departmentName).FirstOrDefault();
                if (objedk != null)
                {
                    crude_class_library.NoteApproversMaster newDepartmentAdmin = new crude_class_library.NoteApproversMaster();
                    //var newDepartmentAdmin = new DepartmentAdmin();
                    newDepartmentAdmin.DepartmentId = objedk.DepartmentId;
                    newDepartmentAdmin.ApproverType = noteapprovers.ApproverType;
                    newDepartmentAdmin.ApproverEmail = noteapprovers.ApproverEmail;
                    newDepartmentAdmin.CreatedBy = noteapprovers.CreatedBy;
                    newDepartmentAdmin.ModifiedBy = noteapprovers.ModifiedBy;
                    newDepartmentAdmin.SecretaryEmail = noteapprovers.SecretaryEmail;
                    newDepartmentAdmin.ApproverOrder = noteapprovers.ApproverOrder;
                    newDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                    newDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                    _context.NoteApproversMaster.Add(newDepartmentAdmin);
                    _context.SaveChanges();


                }
                return noteapprovers;
              
            }
            catch (Exception ex)
            {
                return noteapprovers;
            }
        }


        //Edit
        [HttpPost]
        [Route("Edit")]
        public Models.NoteApproversMaster Edit(Models.NoteApproversMaster noteapprovers)
        {
            try
            {
                if (noteapprovers.NoteApproverMasterId != null)
                {
                    var existingDepartmentAdmin = _context.NoteApproversMaster
                        .Where(d => d.NoteApproverMasterId == noteapprovers.NoteApproverMasterId)
                        .FirstOrDefault();

                    if (existingDepartmentAdmin != null)
                    {

                        existingDepartmentAdmin.DepartmentId = noteapprovers.DepartmentId;
                        existingDepartmentAdmin.ApproverType = noteapprovers.ApproverType;
                        existingDepartmentAdmin.ApproverEmail = noteapprovers.ApproverEmail;
                        existingDepartmentAdmin.SecretaryEmail = noteapprovers.SecretaryEmail;
                        existingDepartmentAdmin.ApproverOrder = noteapprovers.ApproverOrder;
                        existingDepartmentAdmin.CreatedBy = noteapprovers.CreatedBy;
                        existingDepartmentAdmin.ModifiedBy = noteapprovers.ModifiedBy;
                        existingDepartmentAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        existingDepartmentAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                        _context.NoteApproversMaster.Update(existingDepartmentAdmin);
                        _context.SaveChanges();
                    }
                }

                return noteapprovers;
            }
            catch (Exception ex)
            {
                return noteapprovers;
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var noteapprovers = _context.NoteApproversMaster.Find(id);

                if (noteapprovers == null)
                {
                    return NotFound("Department Admin not found");
                }

                _context.NoteApproversMaster.Remove(noteapprovers);
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