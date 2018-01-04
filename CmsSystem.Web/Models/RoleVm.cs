using System;
using System.ComponentModel.DataAnnotations;

namespace CmsSystem.Web.Models
{
    public class RoleVm
    {
        public int Id { get; set; }

        [Display(Name = "Tên quyền")]
        [Required(ErrorMessage = "Tên quyền không được bỏ trống")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
    }
}