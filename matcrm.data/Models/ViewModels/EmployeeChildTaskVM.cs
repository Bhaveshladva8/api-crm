using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Tables;

namespace matcrm.data.Models.ViewModels
{
    public class EmployeeChildTaskVM
    {
        public EmployeeChildTaskVM(){
            Comments = new List<EmployeeChildTaskCommentDto>();
            Activities = new List<EmployeeChildTaskActivityDto>();
            AssignedUsers = new List<EmployeeChildTaskUserDto>();
            Documents = new List<EmployeeChildTaskAttachment>();
        }
         public long Id { get; set; }
        public long? WeClappTimeRecordId { get; set; }
        public long? EmployeeSubTaskId { get; set; }
        public long? StatusId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // public bool IsActive { get; set; }
        // public long? CreatedBy { get; set; }
        // public bool IsExpanded { get; set; }
        // public bool IsEdit { get; set; }
        public long? Duration { get; set; }
        // public decimal? Hours { get; set; }
        // public decimal? Minutes { get; set; }
        // public decimal? Seconds { get; set; }  
        public List<EmployeeChildTaskUserDto> AssignedUsers { get; set; }
        public List<EmployeeChildTaskAttachment> Documents { get; set; }
        public List<EmployeeChildTaskActivityDto> Activities { get; set; }
        public List<EmployeeChildTaskCommentDto> Comments { get; set; }
    }
}