using crude_class_library;
using IB_Crude_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATRCreatorsController : ControllerBase
    {
        IBDatabaseDbContext _context;
        public IConfiguration _Configuration;
        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public ATRCreatorsController(IBDatabaseDbContext context, IConfiguration configuration)
        {
            _context = context;
            _Configuration = configuration;
        }



         [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var aTRCreators = _context.ATRCreators.Find(id);

                if (aTRCreators == null)
                {
                    return NotFound("Entity not found");
                }

                return Ok(aTRCreators);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving entity: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public void Create(Models.ATRCreators e_AtrCreator)
        {
            try
            {
                crude_class_library.ATRCreators aTRCreators = new crude_class_library.ATRCreators();
                aTRCreators.ATRCreatorEmail = e_AtrCreator.ATRCreatorEmail;
                aTRCreators.CreatedBy = e_AtrCreator.CreatedBy;
                aTRCreators.ModifiedBy = e_AtrCreator.ModifiedBy;
                aTRCreators.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                aTRCreators.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                _context.ATRCreators.Add(aTRCreators);
                _context.SaveChanges();

                crude_class_library.EmailTemplates objMail = _context.EmailTemplates.Where(t => t.MailFor == "Create").FirstOrDefault();

                if (objMail != null)
                {
                    SMTPMail objsmtp = new SMTPMail();
                    objsmtp.ToEmail = "vipulkumar@xencia.com";
                    objsmtp.CCEmail = e_AtrCreator.CreatedBy;
                    // if (secratarymail != null)
                    // {
                    //     objsmtp.CCEmail = secratarymail + ";" + e_AtrCreator.CreatedBy;
                    // }
                    // else
                    // {
                    //     objsmtp.CCEmail = e_AtrCreator.CreatedBy;
                    // }
                    string modifiedSubject = objMail.Subject.Replace("XXXX", e_AtrCreator.ATRCreatorEmail);
                    objsmtp.Subject = modifiedSubject;
                    // int linkIndex = objMail.Body.IndexOf(_Configuration["SMTPConfig:EnoteView"]);
                    // string modifiedLink = _Configuration["SMTPConfig:EnoteView"] + e_AtrCreator.ATRCreatorEmail.ToString();
                    // string IntraNetLink = _Configuration["SMTPConfig:EnoteViewIntraNet"] + e_AtrCreator.ATRCreatorEmail.ToString();
                    string modifiedBody = objMail.Body
                         .Replace("CCCC", e_AtrCreator.ATRCreatorEmail)
                         .Replace("DDDD", e_AtrCreator.CreatedBy)
                         .Replace("EEEE", e_AtrCreator.ModifiedBy);
                    objsmtp.TextBody = modifiedBody;
                    objsmtp.AttachmentPath = "";
                    SendEmailController.SendEmailAsync(_Configuration, objsmtp);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
            }
        }
        //public Models.ATRCreators Create(Models.ATRCreators e_AtrCreator)
        //{
        //    try
        //    {
        //        crude_class_library.ATRCreators aTRCreators = new crude_class_library.ATRCreators();
        //        aTRCreators.ATRCreatorEmail = e_AtrCreator.ATRCreatorEmail;
        //        aTRCreators.CreatedBy = e_AtrCreator.CreatedBy;
        //        aTRCreators.ModifiedBy = e_AtrCreator.ModifiedBy;
        //        aTRCreators.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
        //        aTRCreators.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
        //        _context.ATRCreators.Add(aTRCreators);
        //        _context.SaveChanges();

        //        crude_class_library.EmailTemplates objMail = _context.EmailTemplates.Where(t => t.MailFor == "Create").FirstOrDefault();

        //        if (objMail != null)
        //        {
        //            SMTPMail objsmtp = new SMTPMail();
        //            objsmtp.ToEmail = "vipulkumar@xencia.com";
        //            objsmtp.CCEmail = e_AtrCreator.CreatedBy;
        //            //if (secratarymail != null)
        //            //{
        //            //    objsmtp.CCEmail = secratarymail + ";" + e_AtrCreator.CreatedBy;

        //            //}
        //            //else
        //            //{
        //            //    objsmtp.CCEmail = e_AtrCreator.CreatedBy;
        //            //}
        //            string modifiedSubject = objMail.Subject.Replace("XXXX", e_AtrCreator.ATRCreatorEmail);
        //            objsmtp.Subject = modifiedSubject;
        //            //int linkIndex = objMail.Body.IndexOf(_Configuration["SMTPConfig:EnoteView"]);
        //            //string modifiedLink = _Configuration["SMTPConfig:EnoteView"] + e_AtrCreator.ATRCreatorEmail.ToString();
        //            //string IntraNetLink = _Configuration["SMTPConfig:EnoteViewIntraNet"] + e_AtrCreator.ATRCreatorEmail.ToString();
        //            string modifiedBody = objMail.Body
        //                 .Replace("CCCC", e_AtrCreator.ATRCreatorEmail)
        //                 .Replace("DDDD", e_AtrCreator.CreatedBy)
        //                 .Replace("EEEE", e_AtrCreator.ModifiedBy);
        //            objsmtp.AttachmentPath = "";
        //            SendEmailController.SendEmailAsync(_Configuration, objsmtp);
        //        }


        //        return e_AtrCreator;
        //    }
        //    catch (Exception ex)
        //    {
        //        return e_AtrCreator;
        //    }

        //}

        [HttpPost]
        [Route("Edit")]
        public Models.ATRCreators Edit(Models.ATRCreators e_AtrCreator)
        {
            try
            {
                if(e_AtrCreator.ATRCreatorId != null)
                {
                    crude_class_library.ATRCreators aTRCreators = _context.ATRCreators.Where(t => t.ATRCreatorId == e_AtrCreator.ATRCreatorId).FirstOrDefault();
                    if(aTRCreators != null)
                    {
                        aTRCreators.ATRCreatorEmail = e_AtrCreator.ATRCreatorEmail;
                        aTRCreators.CreatedBy = e_AtrCreator.CreatedBy;
                        aTRCreators.ModifiedBy = e_AtrCreator.ModifiedBy;
                        aTRCreators.ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        aTRCreators.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
                        _context.ATRCreators.Update(aTRCreators);
                        _context.SaveChanges();
                       
                    }
                    
                }
                return e_AtrCreator;


            }
            catch (Exception ex)
            {
                return e_AtrCreator;
            }
            
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var aTRCreators = _context.ATRCreators.Find(id);

                if (aTRCreators == null)
                {
                    return NotFound("Entity not found");
                }

                _context.ATRCreators.Remove(aTRCreators);
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
