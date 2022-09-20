﻿using Microsoft.AspNetCore.Http;
using matcrm.data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matcrm.data.Models.Response
{
    public class SendEmailResponse
    {        
        public string ErrorMessage { get; set; } = "";        
        public bool IsValid { get; set; } = true;        
    }
}