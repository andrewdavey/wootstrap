using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Mvc.MetadataExtensions;
using System.ComponentModel.DataAnnotations;

namespace Wootstrap.Areas.Public.ViewModels
{
    public class ExampleForm
    {
        public string SomeText { get; set; }
        public bool NiceBoolean { get; set; }
        public string LargeText { get; set; }
        public int PickNumber { get; set; }
        public int? MaybePickNumber { get; set; }
        public decimal Price { get; set; }
        public string Password { get; set; }
    }

    public class ExampleFormMetadata : AbstractValidator<ExampleForm>
    {
        public ExampleFormMetadata()
        {
            RuleFor(f => f.LargeText).DataType(DataType.MultilineText);
            RuleFor(f => f.Password).DataType(DataType.Password);
        }
    }
}