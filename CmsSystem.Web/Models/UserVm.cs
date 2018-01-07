using System;
using System.ComponentModel.DataAnnotations;

namespace CmsSystem.Web.Models
{
    public class UserVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        public string Salt { get; set; }

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        public string Mobile { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}