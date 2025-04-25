using Application.Features.Auth.Commands;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Tên người dùng không được bỏ trống.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email không được bỏ trống.")
            .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được bỏ trống.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone không hợp lệ"); // regex bắt đầu = 0 và độ dài 10-15 kí tự

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
                .MinimumLength(5).WithMessage("Tên tài khoản không hợp lệ.")
                .MaximumLength(20).WithMessage("Tên tài khoản không hợp lệ.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Tên tài khoản không được chứa ký tự đặc biệt."); // regex không chứa các kí tự đặt biệt

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được bỏ trống.")
                .MinimumLength(5).WithMessage("Mật khẩu không hợp lệ.")
                .MaximumLength(20).WithMessage("Mật khẩu không hợp lệ.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Mật khẩu không được chứa ký tự đặc biệt.");
        }
    }
}
