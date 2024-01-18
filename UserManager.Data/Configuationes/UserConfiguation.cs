using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Data.Entities;

namespace UserManager.Data.Configuationes
{
    public class UserConfiguation : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(p=>p.Email).HasMaxLength(128);
           
        }
    }
}
