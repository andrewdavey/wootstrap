using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Mvc.MetadataExtensions;
using System.ComponentModel.DataAnnotations;

namespace Wootstrap.Areas.Public.ViewModels
{
    public class SignInViewModelMetadata : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelMetadata()
        {
            RuleFor(m => m.Password).DataType(DataType.Password);
        }
    }
}