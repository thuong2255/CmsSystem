using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("Functions")]
    public class Function
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255), Column(TypeName = "VARCHAR")]
        [Required]
        public string Code { get; set; }

        [StringLength(500), Column(TypeName = "NVARCHAR")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual IEnumerable<FunctionRole> FunctionRoles { get; set; }
    }
}
