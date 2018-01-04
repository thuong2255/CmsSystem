using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CmsSystem.Model.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150), Column(TypeName = "NVARCHAR")]
        [Required]
        public string Name { get; set; }

        [StringLength(500), Column(TypeName = "NVARCHAR")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual IEnumerable<RoleUser> RoleUsers { get; set; }

        public IEnumerable<FunctionRole> FunctionRoles { get; set; }
    }
}
