using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("Actions")]
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "NVARCHAR")]
        public string Name { get; set; }

        [StringLength(255), Column(TypeName = "VARCHAR")]
        public string Link { get; set; }
          
        [StringLength(500), Column(TypeName = "NVARCHAR")]
        public string Description { get; set; }

        [StringLength(100), Column(TypeName = "VARCHAR")]
        public string Icon { get; set; }

        public int? ParentId { get; set; }

        public bool Display { get; set; }

        public int Position { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual IEnumerable<ActionRole> ActionRoles { get; set; }

    }
}
