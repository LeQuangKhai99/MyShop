using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản!")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}