using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace matcrm.data.Models.Tables {
    public class ActivityType {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column (TypeName = "varchar(150)")]
        public string? Name { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}