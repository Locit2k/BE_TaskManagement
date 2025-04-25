using Application.Features.Auth.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Tài khoản không được bỏ trống.")
            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Tài khoản không được chứa ký tự đặc biệt.");

            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Mật khẩu không được bỏ trống.")
           .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Mật khẩu không được chứa ký tự đặc biệt.");
        }
    }
}
