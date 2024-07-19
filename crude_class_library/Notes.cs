using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndianBank_API
{
    public class Notes
    {
        [Key]
        public int noteId { get; set; }
        public int noteFor { get; set; }
        public string noteNumber { get; set; }
        public int status { get; set; }
        public int departmentId { get; set; }
        public int? CommitteeId { get; set; }
        public int? BoardCommitteeId { get; set; }
        public int? noteTo { get; set; }
        public string? Subject { get; set; }
        public int natureofNote { get; set; }
        public int? natureOfApprovalOrSanction { get; set; }
        public int noteType { get; set; }
        public int? financialType { get; set; }
        public Double? amount { get; set; }
        public string? searchKeyword { get; set; }
        public string? Purpose { get; set; }
        public string? DraftResolution { get; set; }
        public string? NotePdfPath { get; set; }
        public string? NoteWordPath { get; set; }
        public string CurrentActioner { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        public int? MeetingStatus { get; set; }
    }
}
