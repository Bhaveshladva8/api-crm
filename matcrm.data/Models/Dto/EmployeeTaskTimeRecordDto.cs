using System;

namespace matcrm.data.Models.Dto {
    public class EmployeeTaskTimeRecordDto {
        public long? Id { get; set; }
        public int? UserId { get; set; }
        public long? Duration { get; set; }
        public long? EmployeeTaskId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}