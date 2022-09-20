using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using matcrm.data.Models.Request;
using matcrm.data.Models.Tables;
using matcrm.data.Models.ViewModels;

namespace matcrm.data.Models.Response
{
    public class EmployeeTaskListResponse
    {
       public long Id { get; set; }
       public string Description { get; set; }
       public long? StatusId { get; set; }
       public string Status { get; set; }
       public DateTime? CreatedOn { get; set; }
       public List<EmployeeTaskUserRequestResponse> AssignedUsers { get; set; }
    }
}