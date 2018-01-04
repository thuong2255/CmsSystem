using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Models
{
    public class FunctionVm
    {

        public int Id { get; set; }

        [Display(Name ="Code")]
        [Required(ErrorMessage ="Code không được bỏ trống")]
        public string Code { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

    }
}