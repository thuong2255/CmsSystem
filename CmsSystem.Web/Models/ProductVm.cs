using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Models
{
    public class ProductVm
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ProductCategoryVm ProductCategory { get; set; }
    }
}