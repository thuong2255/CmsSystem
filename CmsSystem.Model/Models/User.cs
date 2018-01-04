using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "VARCHAR")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "VARCHAR")]
        public string Password { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "VARCHAR")]
        public string Salt { get; set; }

        [StringLength(100), Column(TypeName = "NVARCHAR")]
        public string FullName { get; set; }

        [StringLength(150), Column(TypeName = "NVARCHAR")]
        public string Address { get; set; }

        [StringLength(100), Column(TypeName = "VARCHAR")]
        public string Email { get; set; }

        [StringLength(20), Column(TypeName = "VARCHAR")]
        public string Mobile { get; set; }

        [StringLength(200), Column(TypeName = "NVARCHAR")]
        public string Description { get; set; }

        public bool IsAdmin { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual IEnumerable<RoleUser> RoleUsers { get; set; }
    }
}