using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Mvc.MetadataExtensions;
using FluentValidation.Results;
using Wootstrap.Infrastructure.Metadata;

namespace Wootstrap.Areas.Public.ViewModels
{
    public class SignInViewModelMetadata : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelMetadata()
        {
            RuleFor(m => m.Username).Description("Try 'test'");
            RuleFor(m => m.Password).DataType(DataType.Password).Description("Try 'password'");
            RuleFor(m => m.ReturnUrl).HiddenInput(displayValue: false);
            AddRule(new AuthenticateUsernameAndPassword());
        }

        class AuthenticateUsernameAndPassword : IValidationRule<SignInViewModel>
        {
            public IEnumerable<ValidationFailure> Validate(ValidationContext<SignInViewModel> context)
            {
                var model = context.InstanceToValidate;
                if (!(model.Username == "test" && model.Password == "password"))
                {
                    // Return a model level validation failure.
                    yield return new ValidationFailure("", "Incorrect username or password.");
                }
            }
        }
    }
}