using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("ActionRoles")]
    public class ActionRole
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ActionId { get; set; }


        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("ActionId")]
        public virtual Action Action { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
