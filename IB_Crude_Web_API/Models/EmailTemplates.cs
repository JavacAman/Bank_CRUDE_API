using System.ComponentModel.DataAnnotations;

namespace IB_Crude_Web_API.Models
{
    public class EmailTemplate
    {
        [Key]
        public int EmailTemplateId { get; set; }
        public string MailFor { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public string NoteDetails { get; set; }
        //public string Regards { get; set; }
    }
}
