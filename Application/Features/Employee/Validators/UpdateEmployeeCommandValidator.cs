using Application.Features.Employee.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeID)
                  .NotEmpty().WithMessage("Mã nhân viên không được bỏ trống");

            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage("Tên nhân viên không được bỏ trống");

            RuleFor(x => x.Gender)
               .NotEmpty().WithMessage("Giới tính không được bỏ trống");

            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống");

            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("Số điện thoại không được bỏ trống");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email không được bỏ trống");

            RuleFor(x => x.Address)
               .NotEmpty().WithMessage("Địa chỉ không được bỏ trống");

        }
    }
}
