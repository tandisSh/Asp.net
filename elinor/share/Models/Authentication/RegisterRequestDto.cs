using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Authentication
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "لطفا شماره همراه را وارد نمایید")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا نام  را وارد نمایید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        public string LastName { get; set; }
    }
}
