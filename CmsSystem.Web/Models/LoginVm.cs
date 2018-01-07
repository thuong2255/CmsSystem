using System.ComponentModel.DataAnnotations;

namespace CmsSystem.Web.Models
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password không được bỏ trống")]
        public string Password { get; set; }
    }
}