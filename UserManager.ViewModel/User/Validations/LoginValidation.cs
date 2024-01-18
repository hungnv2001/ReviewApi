using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.ViewModel.User.Validations
{
    public class LoginValidation: AbstractValidator<LoginRequest>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserNane cần nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu phải có").MinimumLength(5).WithMessage("Min length là 6");
        }
    }
}
