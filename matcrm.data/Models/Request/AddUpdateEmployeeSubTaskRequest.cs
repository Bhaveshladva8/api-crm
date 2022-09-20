using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using matcrm.data.Models.Tables;

namespace matcrm.data.Models.Request
{
    public class AddUpdateEmployeeSubTaskRequest
    {
        public long? Id { get; set; }
        public long? EmployeeTaskId { get; set; }
        public long? StatusId { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<EmployeeSubTaskUserRequestResponse> AssignedUsers { get; set; }
    }
}