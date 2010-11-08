using System.Reflection;
using Autofac;
using FluentValidation;

namespace Wootstrap.Infrastructure.Ioc
{
    public class MetadataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AsClosedTypesOf(typeof(AbstractValidator<>));
        }
    }
}