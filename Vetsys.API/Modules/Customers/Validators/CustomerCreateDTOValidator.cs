using FluentValidation;
using Vetsys.API.Modules.Customers.DTOs;

namespace Vetsys.API.Modules.Customers.Validators
{
    public class CustomerCreateDTOValidator : AbstractValidator<CustomerCreateDTO>
    {
        public CustomerCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must be at most 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be valid.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}