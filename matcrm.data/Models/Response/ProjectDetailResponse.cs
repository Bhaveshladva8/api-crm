using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Request;
using matcrm.data.Models.ViewModels;

namespace matcrm.data.Models.Response
{
    public class ProjectDetailResponse
    {   public ProjectDetailResponse()
        {
            AssignedUsers = new List<EmployeeProjectUserRequestResponse>();
            //Tasks = new List<EmployeeTaskVM>();            
            //Sections = new List<EmployeeSectionVM>();
        }
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public string? LogoURL { get; set; }        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? Priority { get; set; }
        public long? StatusId { get; set; }
        public string StatusName { get; set; }
        public string UserName { get; set; }
        public string UserProfileURL { get; set; }
        public long? TaskCount { get; set; }
        public List<EmployeeProjectUserRequestResponse> AssignedUsers { get; set; }       
        //public List<EmployeeTaskVM> Tasks { get; set; }
        
        //public List<EmployeeSectionVM> Sections { get; set; }
    }
}