using System;
using System.Collections.Generic;

namespace CmsSystem.Web.Models
{
    public class ProductCategoryVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual IEnumerable<ProductVm> Products { get; set; }
    }
}