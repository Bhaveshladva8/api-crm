using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matcrm.data.Models.Request
{
    public class DiscussionParticipantRequest
    {
        public long? Id { get; set; }
        public long? DiscussionId { get; set; }
        public int? TeamMemberId { get; set; }
       
    }
}
