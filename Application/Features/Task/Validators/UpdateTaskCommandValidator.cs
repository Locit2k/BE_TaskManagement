using Application.Features.Task.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Task.Validators
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Tên công việc không bỏ trống");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Ngày bắt đầu không bỏ trống");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Ngày hết hạn không bỏ trống");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Tình trạng không bỏ trống");
        }
    }
}
