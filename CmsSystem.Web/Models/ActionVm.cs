using System;
using System.ComponentModel.DataAnnotations;

namespace CmsSystem.Web.Models
{
    public class ActionVm
    {
        public int Id { get; set; }

        [Display(Name = "Chức năng")]
        [Required(ErrorMessage = "Tên chức năng không được bỏ trống")]
        public string Name { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Icon")]
        public string Icon { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Display { get; set; }

        [Display(Name = "Thứ tự")]
        [Required(ErrorMessage = "Thứ tự không được bỏ trống")]
        public int Position { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
    }
}