using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matcrm.data.Models.Request
{
    public class SendReplyRequest
    {
        public SendReplyRequest()
        {
            
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            FileList = new IFormFile[] { };
        }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Provider { get; set; }
        public string ProviderApp { get; set; }
        public string SelectedEmail { get; set; }
        public int UserId { get; set; }
        public string threadId { get; set; }
        public IFormFile[] FileList { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public string From { get; set; }
    }
}
