using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using matcrm.data.Models.Tables;

namespace matcrm.data.Models.Request
{
    public class AddUpdateEmployeeTaskRequest
    {
        // public long? Id { get; set; }
        // public long? OneClappTaskId { get; set; }
        // public long? ProjectId { get; set; }
        // public bool IsActive { get; set; }
        // public DateTime? StartDate { get; set; }
        // public DateTime? EndDate { get; set; }
        // public long? SectionId { get; set; }
        // public double? Duration { get; set; }
        // public long? StatusId { get; set; }
        // public string Description { get; set; }
        // public double? StartAt { get; set; }
        // public long? Priority { get; set; }
        // public List<EmployeeTaskUserRequestResponse> AssignedUsers { get; set; }

        public AddUpdateEmployeeTaskRequest()
        {           
            AssignedUsers = new List<EmployeeTaskUserRequestResponse>();
        }

        public long? Id { get; set; }
        public string Description { get; set; }
        public long? ProjectId { get; set; }
        public long? SectionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public double? Duration { get; set; }
        public long? StatusId { get; set; }
        public List<EmployeeTaskUserRequestResponse> AssignedUsers { get; set; }
    }
}