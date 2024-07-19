using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationsController : ControllerBase
    {
        IBDatabaseDbContext _dbContext;
        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DesignationsController(IBDatabaseDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("getDesignations")]
        public List<Models.Designations> getDesignations()
        {
            List<Models.Designations> deslist = new List<Models.Designations>();
            try
            {
                deslist = _dbContext.Designations.Select(c => new Models.Designations
                {
                    Designation = c.Designation,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedDate = c.ModifiedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                // LogsController.AddLogs(_dbContext, "department/getDepartment", ex.Message, ex.GetBaseException().StackTrace);
            }

            return deslist;
        }


        [HttpPost]
        [Route("addDesignations")]
        public Models.Designations addDesignations(Models.Designations designations)
        {
            try
            {
                if (designations != null)
                {

                    crude_class_library.Designations objDesignationsnames = _dbContext.Designations.FirstOrDefault(t => t.Designation == designations.Designation);


                    

                    if (objDesignationsnames == null)
                    {
                        crude_class_library.Designations objDesignations = new crude_class_library.Designations();
                        objDesignations.Designation = designations.Designation;
                        objDesignations.CreatedBy = designations.CreatedBy;
                        objDesignations.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        objDesignations.ModifiedBy = designations.ModifiedBy;
                        objDesignations.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _dbContext.Designations.Add(objDesignations);
                        _dbContext.SaveChanges();
                        //committee.statusMessage = "Success";
                    }
                    else
                    {
                        //committee.statusMessage = "Duplicate";

                    }
                }
            }
            catch (Exception ex)
            {
                

            }
            return designations;
        }

        [HttpPost]
        [Route("updateDesignations")]
        public Models.Designations updateDesignations(Models.Designations designations)
        {
            try
            {
                if (designations != null)
                {

                    crude_class_library.Designations objDesignations = _dbContext.Designations.FirstOrDefault(t => t.DesignationId == designations.DesignationId);

                    if (objDesignations != null)
                    {
                        objDesignations.Designation = designations.Designation;
                        objDesignations.CreatedBy = designations.CreatedBy;
                        objDesignations.ModifiedBy = designations.ModifiedBy;
                        objDesignations.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _dbContext.Designations.Update(objDesignations);
                        _dbContext.SaveChanges();
                        //committee.statusMessage = "Success";
                    }
                    else
                    {
                        //committee.statusMessage = "Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                

            }
            return designations;
        }

        [HttpPost]
        [Route("deleteDesignations")]
        public Models.Designations deleteCommittee(Models.Designations designations)
        {
            try
            {
                if (designations != null)
                {
                    crude_class_library.Designations objDesignations = _dbContext.Designations.FirstOrDefault(t => t.DesignationId == designations.DesignationId);

                    if (objDesignations != null)
                    {
                        _dbContext.Designations.Remove(objDesignations);
                        _dbContext.SaveChanges();
                        //committee.statusMessage = "Success";
                    }
                    else
                    {
                        //committee.statusMessage = "Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                

            }
            return designations;
        }
    }
}
