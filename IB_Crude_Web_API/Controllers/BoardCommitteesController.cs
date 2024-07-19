using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardCommitteesController : ControllerBase
    {
        IBDatabaseDbContext _context;
        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public BoardCommitteesController(IBDatabaseDbContext context)
        {
            _context = context;
        }


// Get Method
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var bCommittee = _context.BoardCommittees.Find(id);

                if (bCommittee == null)
                {
                    return NotFound("Entity not found");
                }

                return Ok(bCommittee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving entity: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public Models.BoardCommittees Create(Models.BoardCommittees e_bCommittee)
        {
            try
            {
                crude_class_library.BoardCommittees bCommittee = new crude_class_library.BoardCommittees();
                bCommittee.BoardCommitteeName = e_bCommittee.BoardCommitteeName;
                bCommittee.CreatedBy = e_bCommittee.CreatedBy;
                bCommittee.ModifiedBy = e_bCommittee.ModifiedBy;
                bCommittee.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                bCommittee.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                _context.BoardCommittees.Add(bCommittee);
                _context.SaveChanges();
                return e_bCommittee;
            }
            catch (Exception ex)
            {
                return e_bCommittee;
            }

        }

        [HttpPost]
        [Route("Edit")]
        public Models.BoardCommittees Edit(Models.BoardCommittees e_bCommittee)
        {
            try
            {
                if (e_bCommittee.BoardCommitteeId != null)
                {
                    crude_class_library.BoardCommittees bCommittee = _context.BoardCommittees.Where(t => t.BoardCommitteeId == e_bCommittee.BoardCommitteeId).FirstOrDefault();
                    if (bCommittee != null)
                    {
                        bCommittee.BoardCommitteeName = e_bCommittee.BoardCommitteeName;
                        bCommittee.CreatedBy = e_bCommittee.CreatedBy;
                        bCommittee.ModifiedBy = e_bCommittee.ModifiedBy;
                        bCommittee.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        bCommittee.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _context.BoardCommittees.Update(bCommittee);
                        _context.SaveChanges();

                    }

                }
                return e_bCommittee;


            }
            catch (Exception ex)
            {
                return e_bCommittee;
            }

        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var bCommittee = _context.BoardCommittees.Find(id);

                if (bCommittee == null)
                {
                    return NotFound("Entity not found");
                }

                _context.BoardCommittees.Remove(bCommittee);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting entity: " + ex.Message);
            }
        }
    }
}
