using Autofac;
using Autofac.Integration.Web;
using Wootstrap.Infrastructure.Ioc;

namespace Wootstrap.Infrastructure.Initialization
{
    public static class Ioc
    {
        /// <summary>
        /// Creates and configures a new Autofac Container for the application.
        /// </summary>
        public static IContainer CreateApplicationContainer()
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            return builder.Build();
        }

        static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SystemWebModule>();
            builder.RegisterModule<MetadataModule>();
            // TODO: Register more types, modules, etc, with the container builder.
        }
    }
}