using System;
using Autofac;
using Autofac.Integration.Web;
using FluentValidation;

namespace Wootstrap.Infrastructure.Metadata
{
    /// <summary>
    /// Creates validators using the convention that validator is a class with the name
    /// "{ModelTypeName}Metadata"
    /// </summary>
    public class ConventionalValidatorFactory : IValidatorFactory
    {
        public ConventionalValidatorFactory(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

        readonly IContainerProvider containerProvider;

        public IValidator GetValidator(Type type)
        {
            var validatorType = typeof(AbstractValidator<>).MakeGenericType(type);
            if (validatorType == null) return null;
            return (IValidator)containerProvider.RequestLifetime.Resolve(validatorType);
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }
    }
}