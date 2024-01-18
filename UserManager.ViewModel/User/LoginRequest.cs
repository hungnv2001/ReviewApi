using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.ViewModel.User
{
    public class LoginRequest
    {
        public string UserName {  get; set; }
        [MinLength(5,ErrorMessage ="minlength= 5")]
        public string Password { get; set; }
        public bool RememberMe {  get; set; }
    }
}
