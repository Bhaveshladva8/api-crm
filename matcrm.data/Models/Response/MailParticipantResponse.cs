﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matcrm.data.Models.Response
{
    public class MailParticipantResponse
    {       
        public long Id { get; set; }       
        public int? TeamMemberId { get; set; }       
        public string? ThreadId { get; set; }
    }

}
