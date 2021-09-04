namespace App.Configuration
{
    public class Settings
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string TextEmailBody { get; set; }
        public string HtmlEmailBody { get; set; }
        public string TemplateId { get; set; }
        public string AttachmentFilePath { get; set; }
    }
}
