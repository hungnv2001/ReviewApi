using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }

}
