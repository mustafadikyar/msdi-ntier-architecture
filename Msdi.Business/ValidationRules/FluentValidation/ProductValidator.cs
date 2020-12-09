using FluentValidation;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Title).NotNull();
            RuleFor(c => c.Title).MinimumLength(5);
        }
    }
}
