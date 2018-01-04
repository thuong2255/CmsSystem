using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsSystem.Model.Models
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "NVARCHAR")]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
