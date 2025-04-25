using Application.Features.Auth.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using System.Net.NetworkInformation;
using Application.Features.Employee.Validators;
namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
