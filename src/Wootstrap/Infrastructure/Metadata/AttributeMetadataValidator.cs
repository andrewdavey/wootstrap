using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Mvc;
using FluentValidation.Resources;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Wootstrap.Infrastructure.Metadata
{
    /// <remarks>
    /// Copied from FluentValidation.Mvc source - For some reason it's internal there - bah!
    /// https://github.com/JeremySkinner/FluentValidation
    /// </remarks> 
    class AttributeMetadataValidator : IAttributeMetadataValidator
    {
        readonly Attribute attribute;

        public AttributeMetadataValidator(Attribute attributeConverter)
        {
            attribute = attributeConverter;
        }

        public IErrorMessageSource ErrorMessageSource
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            return Enumerable.Empty<ValidationFailure>();
        }

        public string ErrorMessageTemplate
        {
            get { return null; }
            set { }
        }

        public ICollection<Func<object, object>> CustomMessageFormatArguments
        {
            get { return null; }
        }

        public bool SupportsStandaloneValidation
        {
            get { return false; }
        }

        public Func<object, object> CustomStateProvider
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Attribute ToAttribute()
        {
            return attribute;
        }
    }
}