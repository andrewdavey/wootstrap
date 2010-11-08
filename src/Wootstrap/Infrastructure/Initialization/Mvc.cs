using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using FluentValidation.Mvc;
using Wootstrap.Infrastructure.Metadata;

namespace Wootstrap.Infrastructure.Initialization
{
    public static class Mvc
    {
        public static void Initialize(IContainerProvider containerProvider)
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterModelValidation(ModelValidatorProviders.Providers, containerProvider);
            RegisterControllerFactory(containerProvider);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var publicRoute = routes.MapRoute(
                "Public_default",
                "{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional }
            );
            publicRoute.DataTokens["Area"] = "Public";
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
            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(containerProvider));
        }
    }
}