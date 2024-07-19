using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crude_class_library
{
    public class EmailTemplates
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
