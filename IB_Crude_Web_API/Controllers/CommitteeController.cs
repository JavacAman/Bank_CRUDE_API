using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitteeController : ControllerBase
    {
        IBDatabaseDbContext _dbContext;
        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public CommitteeController(IBDatabaseDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("getCommittee")]
        public List<Models.Committee> getCommittee()
        {
            List<Models.Committee> comlist = new List<Models.Committee>();
            try
            {
                comlist = _dbContext.Committees.Select(c => new Models.Committee
                {
                    CommitteeName = c.CommitteeName,
                    CommitteeAlias = c.CommitteeAlias,
                    Convener = c.Convener,
                    ConvenerDepartmentId = c.ConvenerDepartmentId,
                    Chairman = c.Chairman,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedDate = c.ModifiedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                
            }

            return comlist;
        }

        [HttpPost]
        [Route("addCommittee")]
        public Models.Committee addCommittee(Models.Committee committee)
        {
            try
            {
                if (committee != null)
                {
                    crude_class_library.Departments objCommitteenames = _dbContext.Departments.Where(t => t.DepartmentName == committee.departmentName).FirstOrDefault();

                    

                    if (objCommitteenames != null)
                    {
                        crude_class_library.Committees objCommittee = new crude_class_library.Committees();
                        objCommittee.CommitteeName = committee.CommitteeName;
                        objCommittee.CommitteeAlias = committee.CommitteeAlias;
                        objCommittee.Convener = committee.Convener;
                        //objCommittee.ConvenerDepartment = (int)objCommitteenames.DepartmentId;
                        objCommittee.ConvenerDepartmentId = objCommitteenames.DepartmentId;
                        objCommittee.Chairman = committee.Chairman;
                        objCommittee.CreatedBy = committee.CreatedBy;
                        objCommittee.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        objCommittee.ModifiedBy = committee.ModifiedBy;
                        objCommittee.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        
                        _dbContext.Committees.Add(objCommittee);
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
            return committee;
        }




        [HttpPost]
        [Route("updateCommittee")]
        public Models.Committee updateCommittee(Models.Committee committee)
        {
            try
            {
                if (committee != null)
                {
                    crude_class_library.Committees objCommittee = _dbContext.Committees.Where(t => t.CommitteeId == committee.CommitteeId).FirstOrDefault();

                    if (objCommittee != null)
                    {
                        objCommittee.CommitteeName = committee.CommitteeName;
                        objCommittee.CommitteeAlias = committee.CommitteeAlias;
                        objCommittee.Convener = committee.Convener;
                        objCommittee.ConvenerDepartmentId = committee.ConvenerDepartmentId;
                        objCommittee.Chairman = committee.Chairman;
                        objCommittee.CreatedBy = committee.CreatedBy;
                        objCommittee.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        objCommittee.ModifiedBy = committee.ModifiedBy;
                        _dbContext.Committees.Update(objCommittee);
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
            return committee;
        }


        [HttpPost]
        [Route("deleteCommittee")]
        public Models.Committee deleteCommittee(Models.Committee committee)
        {
            try
            {
                if (committee != null)
                {
                    crude_class_library.Committees objCommittee = _dbContext.Committees.Where(t => t.CommitteeId == committee.CommitteeId).FirstOrDefault();

                    if (objCommittee != null)
                    {
                        _dbContext.Committees.Remove(objCommittee);
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
            return committee;
        }
    }
}
