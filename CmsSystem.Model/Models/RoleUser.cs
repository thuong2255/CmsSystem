using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("RoleUsers")]
    public class RoleUser
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int UserId { get; set; }

        
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
