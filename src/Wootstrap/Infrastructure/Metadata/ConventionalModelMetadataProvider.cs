using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Wootstrap.Infrastructure.Metadata
{
    /// <summary>
    /// Provides metadata for model classes using some simple conventions.
    /// For example, expanding pascal cased property names for display names.
    /// </summary>
    /// <remarks>
    /// Based on FluentValidationModelMetadataProvider in FluentValidation.Mvc.
    /// https://github.com/JeremySkinner/FluentValidation
    /// </remarks>
    public class ConventionalModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        readonly IValidatorFactory factory;

        public ConventionalModelMetadataProvider(IValidatorFactory factory)
        {
            this.factory = factory;
        }

        protected override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            var attributes = ConvertFVMetaDataToAttributes(containerType, propertyDescriptor.Name);
            var metadata = CreateMetadata(attributes, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name);
            AssignHumanReadableName(metadata);
            AddDescriptionIfGiven(attributes, metadata);
            return metadata;
        }

        IEnumerable<Attribute> ConvertFVMetaDataToAttributes(Type type, string name)
        {
            var validator = factory.GetValidator(type);
            if (validator == null) return Enumerable.Empty<Attribute>();
            var validators = validator.CreateDescriptor().GetValidatorsForMember(name);

            var attributes = validators.OfType<IAttributeMetadataValidator>()
                .Select(x => x.ToAttribute())
                .Concat(SpecialCaseValidatorConversions(validators));

            return attributes.ToList();
        }

        IEnumerable<Attribute> SpecialCaseValidatorConversions(IEnumerable<IPropertyValidator> validators)
        {
            foreach (var validator in validators)
            {
                if (validator is NotEmptyValidator)
                    yield return new RequiredAttribute();
                if (validator is EmailValidator)
                    yield return new DataTypeAttribute(DataType.EmailAddress);
            }
        }

        void AssignHumanReadableName(ModelMetadata metadata)
        {
            metadata.DisplayName = ExpandPascalCase(metadata);
        }

        string ExpandPascalCase(ModelMetadata metadata)
        {
            return Regex.Replace(metadata.DisplayName ?? metadata.PropertyName ?? "", "(.)([A-Z])", "$1 $2");
        }

        void AddDescriptionIfGiven(IEnumerable<Attribute> attributes, ModelMetadata metadata)
        {
            var descriptionAttribute = attributes.OfType<DescriptionAttribute>().FirstOrDefault();
            if (descriptionAttribute != null)
            {
                metadata.AdditionalValues["Description"] = descriptionAttribute.Description;
            }
        }

    }
}