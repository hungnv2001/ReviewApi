using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.ViewModel.User
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword {  get; set; }
    }
}
