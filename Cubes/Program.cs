using Autofac;
using Cubes.Application.Contracts;
using Cubes.Application.Implementation;
using Cubes.Domain.Contracts;
using Cubes.Domain.Implementation;

namespace Cubes.Presentation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var containerBuilder = BuildDependencies();
            containerBuilder.Resolve<IApp>().Run();
        }

        #region .: Private Methods :.

        private static IContainer BuildDependencies()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);

            var container = containerBuilder.Build();
            ResolveDependencies(container);

            return container;
        }

        private static void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<App>().As<IApp>().SingleInstance();
            containerBuilder.RegisterType<VolumeCalculator>().As<IVolumeCalculator>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<IntersectionCalculator>().As<IIntersectionCalculator>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CubesIntersection>().As<ICubesIntersection>().InstancePerLifetimeScope();
        }

        private static void ResolveDependencies(IContainer container)
        {
            container.Resolve<IVolumeCalculator>();
            container.Resolve<IIntersectionCalculator>();
            container.Resolve<ICubesIntersection>();
        }

        #endregion .: Private Methods :.
    }
}