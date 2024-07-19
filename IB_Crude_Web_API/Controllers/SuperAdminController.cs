using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        IBDatabaseDbContext _dbContext;
        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public SuperAdminController(IBDatabaseDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        [Route("getSuperAdmins")]
        public List<Models.SuperAdmin> getSuperAdmins()
        {
            List<Models.SuperAdmin> superlist = new List<Models.SuperAdmin>();
            try
            {
                superlist = _dbContext.SuperAdmin.Select(c => new Models.SuperAdmin
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    EmailId = c.EmailId,
                    Active = c.Active,
                    Deleted = c.Deleted,
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

            return superlist;
        }



        [HttpPost]
        [Route("addSuperAdmins")]
        public Models.SuperAdmin addSuperAdmins(Models.SuperAdmin superAdmin)
        {
            try
            {
                if (superAdmin != null)
                {

                    crude_class_library.SuperAdmin objSuperAdminnames = _dbContext.SuperAdmin.FirstOrDefault(t => t.FirstName == superAdmin.FirstName);
               

                    if (objSuperAdminnames == null)
                    {
                        crude_class_library.SuperAdmin objSuperAdmin = new crude_class_library.SuperAdmin();
                        objSuperAdmin.FirstName = superAdmin.FirstName;
                        objSuperAdmin.LastName = superAdmin.LastName;
                        objSuperAdmin.EmailId = superAdmin.EmailId;
                        objSuperAdmin.Active = superAdmin.Active;
                        objSuperAdmin.Deleted = superAdmin.Deleted;
                        objSuperAdmin.CreatedBy = superAdmin.CreatedBy;
                        objSuperAdmin.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        objSuperAdmin.ModifiedBy = superAdmin.ModifiedBy;
                        objSuperAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _dbContext.SuperAdmin.Add(objSuperAdmin);
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
            return superAdmin;
        }

        [HttpPost]
        [Route("updateSuperAdmins")]
        public Models.SuperAdmin updateSuperAdmins(Models.SuperAdmin superAdmin)
        {
            try
            {
                if (superAdmin != null)
                {
                    //CrudClassLibrary.Designations objDesignations = _dbContext.Designations.Where(t => t.DesignationId == designations.DesignationId).FirstOrDefault();
                    crude_class_library.SuperAdmin objSuperAdmin = _dbContext.SuperAdmin.FirstOrDefault(t => t.SuperAdminId == superAdmin.SuperAdminId);

                    if (objSuperAdmin != null)
                    {
                        objSuperAdmin.FirstName = superAdmin.FirstName;
                        objSuperAdmin.LastName = superAdmin.LastName;
                        objSuperAdmin.EmailId = superAdmin.EmailId;
                        objSuperAdmin.Active = superAdmin.Active;
                        objSuperAdmin.Deleted = superAdmin.Deleted;
                        objSuperAdmin.CreatedBy = superAdmin.CreatedBy;
                        objSuperAdmin.ModifiedBy = superAdmin.ModifiedBy;
                        objSuperAdmin.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _dbContext.SuperAdmin.Update(objSuperAdmin);
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
            return superAdmin;
        }

        [HttpPost]
        [Route("deleteSuperAdmins")]
        public Models.SuperAdmin deleteSuperAdmins(Models.SuperAdmin superAdmin)
        {
            try
            {
                if (superAdmin != null)
                {
                    crude_class_library.SuperAdmin objSuperAdmin = _dbContext.SuperAdmin.FirstOrDefault(t => t.SuperAdminId == superAdmin.SuperAdminId);

                    if (objSuperAdmin != null)
                    {
                        _dbContext.SuperAdmin.Remove(objSuperAdmin);
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
            return superAdmin;
        }
    }
}
