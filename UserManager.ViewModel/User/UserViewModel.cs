using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.ViewModel.User
{
    public class UserViewModel
    {
        public Guid ID {  get; set; }
        public string Name { get; set; }
        public string UserName {  get; set; }
        public string Email {  get; set; }
    }
}
