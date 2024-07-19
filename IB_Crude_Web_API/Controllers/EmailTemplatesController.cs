using crude_class_library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplatesController : ControllerBase
    {
        private readonly IBDatabaseDbContext _dbContext;

        public EmailTemplatesController(IBDatabaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all EmailTemplates
        [HttpGet]
        [Route("getEmailTemplates")]
        public async Task<List<Models.EmailTemplate>> GetEmailTemplates()
        {
            List<Models.EmailTemplate> emailTemplatesList = new List<Models.EmailTemplate>();
            try
            {
                using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_GetEmailTemplates";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var emailTemplate = new Models.EmailTemplate
                            {
                                EmailTemplateId = dataReader.GetInt32(dataReader.GetOrdinal("EmailTemplateId")),
                                MailFor = dataReader.GetString(dataReader.GetOrdinal("MailFor")),
                                Subject = dataReader.GetString(dataReader.GetOrdinal("Subject")),
                                Body = dataReader.GetString(dataReader.GetOrdinal("Body")),
                                //NoteDetails = dataReader.GetString(dataReader.GetOrdinal("NoteDetails")),
                                //Regards = dataReader.GetString(dataReader.GetOrdinal("Regards"))
                            };
                            emailTemplatesList.Add(emailTemplate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }

            return emailTemplatesList;
        }

        // Add a new EmailTemplate
        [HttpPost]
        [Route("addEmailTemplate")]
        public async Task<IActionResult> AddEmailTemplate([FromBody] Models.EmailTemplate emailTemplate)
        {
            try
            {
                using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_AddEmailTemplate";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@MailFor", SqlDbType.NVarChar) { Value = emailTemplate.MailFor });
                    cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar) { Value = emailTemplate.Subject });
                    cmd.Parameters.Add(new SqlParameter("@Body", SqlDbType.NVarChar) { Value = emailTemplate.Body });
                    //cmd.Parameters.Add(new SqlParameter("@NoteDetails", SqlDbType.NVarChar) { Value = emailTemplate.NoteDetails });
                    //cmd.Parameters.Add(new SqlParameter("@Regards", SqlDbType.NVarChar) { Value = emailTemplate.Regards });

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    var result = await cmd.ExecuteScalarAsync();
                    emailTemplate.EmailTemplateId = Convert.ToInt32(result);

                    return Ok(new { statusMessage = "Success", newEmailTemplateId = emailTemplate.EmailTemplateId });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }
        }

        // Update an existing EmailTemplate
        [HttpPost]
        [Route("updateEmailTemplate")]
        public async Task<IActionResult> UpdateEmailTemplate([FromBody] Models.EmailTemplate emailTemplate)
        {
            try
            {
                using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_UpdateEmailTemplate";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@EmailTemplateId", SqlDbType.Int) { Value = emailTemplate.EmailTemplateId });
                    cmd.Parameters.Add(new SqlParameter("@MailFor", SqlDbType.NVarChar) { Value = emailTemplate.MailFor });
                    cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar) { Value = emailTemplate.Subject });
                    cmd.Parameters.Add(new SqlParameter("@Body", SqlDbType.NVarChar) { Value = emailTemplate.Body });
                    //cmd.Parameters.Add(new SqlParameter("@NoteDetails", SqlDbType.NVarChar) { Value = emailTemplate.NoteDetails });
                    //cmd.Parameters.Add(new SqlParameter("@Regards", SqlDbType.NVarChar) { Value = emailTemplate.Regards });

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { statusMessage = "Success" });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }
        }

        // Delete an EmailTemplate
        [HttpDelete]
        [Route("deleteEmailTemplate/{id}")]
        public async Task<IActionResult> DeleteEmailTemplate(int id)
        {
            try
            {
                using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_DeleteEmailTemplate";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@EmailTemplateId", SqlDbType.Int) { Value = id });

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { statusMessage = "Success" });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

//using crude_class_library;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;
//using System.Collections.Generic;
//using crude_class_library;

//namespace IB_Crude_Web_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class EmailTemplatesController : ControllerBase
//    {
//        private readonly IBDatabaseDbContext _dbContext;
//        private readonly TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

//        public EmailTemplatesController(IBDatabaseDbContext dbContext, IConfiguration configuration)
//        {
//            _dbContext = dbContext;
//        }

//        // Get all EmailTemplates
//        [HttpGet]
//        [Route("getEmailTemplates")]
//        public List<Models.EmailTemplate> getEmailTemplates()
//        {
//            List<Models.EmailTemplate> emailTemplatesList = new List<Models.EmailTemplate>();
//            try
//            {
//                emailTemplatesList = _dbContext.EmailTemplates.Select(t => new Models.EmailTemplate
//                {
//                    EmailTemplateId = t.EmailTemplateId,
//                    MailFor = t.MailFor,
//                    Subject = t.Subject,
//                    Body = t.Body,
//                    //NoteDetails = t.NoteDetails,
//                    //Regards = t.Regards
//                }).ToList();
//            }
//            catch (Exception ex)
//            {
//                // LogsController.AddLogs(_dbContext, "emailTemplates/getEmailTemplates", ex.Message, ex.GetBaseException().StackTrace);
//            }

//            return emailTemplatesList;
//        }

//        // Add a new EmailTemplate
//        [HttpPost]
//        [Route("addEmailTemplate")]
//        public Models.EmailTemplate addEmailTemplate(Models.EmailTemplate emailTemplate)
//        {
//            try
//            {
//                if (emailTemplate != null)
//                {
//                    crude_class_library.EmailTemplates existingEmailTemplate = _dbContext.EmailTemplates
//                        .Where(t => t.MailFor == emailTemplate.MailFor && t.Subject == emailTemplate.Subject).FirstOrDefault();

//                    if (existingEmailTemplate == null)
//                    {
//                        crude_class_library.EmailTemplates newEmailTemplate = new crude_class_library.EmailTemplates
//                        {
//                            MailFor = emailTemplate.MailFor,
//                            Subject = emailTemplate.Subject,
//                            Body = emailTemplate.Body,
//                            //NoteDetails = emailTemplate.NoteDetails,
//                            //Regards = emailTemplate.Regards
//                        };

//                        _dbContext.EmailTemplates.Add(newEmailTemplate);
//                        _dbContext.SaveChanges();
//                        //emailTemplate.statusMessage = "Success";
//                    }
//                    else
//                    {
//                        //emailTemplate.statusMessage = "Duplicate";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //emailTemplate.statusMessage = "Failed";
//                // LogsController.AddLogs(_dbContext, "emailTemplates/addEmailTemplate", ex.Message, ex.GetBaseException().StackTrace);
//            }
//            return emailTemplate;
//        }

//        // Update an existing EmailTemplate
//        [HttpPost]
//        [Route("updateEmailTemplate")]
//        public Models.EmailTemplate updateEmailTemplate(Models.EmailTemplate emailTemplate)
//        {
//            try
//            {
//                if (emailTemplate != null)
//                {
//                    crude_class_library.EmailTemplates existingEmailTemplate = _dbContext.EmailTemplates
//                        .Where(t => t.EmailTemplateId == emailTemplate.EmailTemplateId).FirstOrDefault();

//                    if (existingEmailTemplate != null)
//                    {
//                        existingEmailTemplate.MailFor = emailTemplate.MailFor;
//                        existingEmailTemplate.Subject = emailTemplate.Subject;
//                        existingEmailTemplate.Body = emailTemplate.Body;
//                        //existingEmailTemplate.NoteDetails = emailTemplate.NoteDetails;
//                        //existingEmailTemplate.Regards = emailTemplate.Regards;

//                        _dbContext.EmailTemplates.Update(existingEmailTemplate);
//                        _dbContext.SaveChanges();
//                        //emailTemplate.statusMessage = "Success";
//                    }
//                    else
//                    {
//                        //emailTemplate.statusMessage = "Not Found";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //emailTemplate.statusMessage = "Failed";
//                // LogsController.AddLogs(_dbContext, "emailTemplates/updateEmailTemplate", ex.Message, ex.GetBaseException().StackTrace);
//            }
//            return emailTemplate;
//        }

//        // Delete an EmailTemplate
//        [HttpPost]
//        [Route("deleteEmailTemplate")]
//        public Models.EmailTemplate deleteEmailTemplate(Models.EmailTemplate emailTemplate)
//        {
//            try
//            {
//                if (emailTemplate != null)
//                {
//                    crude_class_library.EmailTemplates existingEmailTemplate = _dbContext.EmailTemplates
//                        .Where(t => t.EmailTemplateId == emailTemplate.EmailTemplateId).FirstOrDefault();

//                    if (existingEmailTemplate != null)
//                    {
//                        _dbContext.EmailTemplates.Remove(existingEmailTemplate);
//                        _dbContext.SaveChanges();
//                        //emailTemplate.statusMessage = "Success";
//                    }
//                    else
//                    {
//                        //emailTemplate.statusMessage = "Not Found";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //emailTemplate.statusMessage = "Failed";
//                // LogsController.AddLogs(_dbContext, "emailTemplates/deleteEmailTemplate", ex.Message, ex.GetBaseException().StackTrace);
//            }
//            return emailTemplate;
//        }
//    }
//}
