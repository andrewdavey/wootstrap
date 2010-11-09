using System.ComponentModel;
using FluentValidation;

namespace Wootstrap.Infrastructure.Metadata
{
    public static class Extensions
    {
        /// <summary>
        /// Defines the additional description text for a property.
        /// </summary>
        public static IRuleBuilder<T, TProperty> Description<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string description)
        {
            return ruleBuilder.SetValidator(
                new AttributeMetadataValidator(new DescriptionAttribute(description))
            );
        }
    }
}