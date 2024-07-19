namespace IB_Crude_Web_API.Models;

public class SMTPMail
{
    public string ToEmail { get; set; }
    public string CCEmail { get; set; }
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string AttachmentPath { get; set; }
}
