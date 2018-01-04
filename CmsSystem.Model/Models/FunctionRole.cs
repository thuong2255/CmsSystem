using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("FunctionRoles")]
    public class FunctionRole
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FunctionId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}
