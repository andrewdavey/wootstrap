using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Web;
using FluentValidation.Mvc;
using Wootstrap.Controllers;
using Wootstrap.Infrastructure.Metadata;

namespace Wootstrap.Infrastructure.Mvc
{
    public static class Wootstrapper
    {
        public static void Initialize(IContainerProvider containerProvider)
        {
            Filtering.Initialize(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            Routing.Initialize(RouteTable.Routes);
            RegisterModelValidation(ModelValidatorProviders.Providers, containerProvider);
            RegisterControllerFactory(containerProvider);
        }

        public static void RegisterModelValidation(ModelValidatorProviderCollection providers, IContainerProvider containerProvider)
        {
            // From http://www.jeremyskinner.co.uk/2010/02/06/fluentvalidation-1-2-beta-2-and-mvc2-rc2/:
            // Disable the DataAnnotationsModelValidatorProvider's "greedy" required rule. 
            // Out of the box, the DataAnnotationsModelValidatorProvider will *always* validate 
            // non-nullable value types, irrespective of whether the property is decorated with a [Required] attribute. 
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            var validatorFactory = new ConventionalValidatorFactory(containerProvider);
            providers.Add(new FluentValidationModelValidatorProvider(validatorFactory));
            ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(validatorFactory);
        }

        public static void RegisterControllerFactory(IContainerProvider containerProvider)
        {
            ControllerBuilder.Current.SetControllerFactory(new WootstrapControllerFactory(containerProvider));
        }
    }
}