using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Mvc.MetadataExtensions;

namespace Wootstrap.Areas.Public.ViewModels
{
    public class SignInViewModelMetadata : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelMetadata()
        {
            RuleFor(m => m.Password).DataType(DataType.Password);
            RuleFor(m => m.ReturnUrl).Scaffold(false);

            RuleFor(m => m.Password).Equal("test").WithMessage("Incorrect username or password.");
        }
    }
}