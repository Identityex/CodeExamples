using AggregatorPackage.Functions;
using Autofac;
using Autofac.Core.Activators.Reflection;
using System.Reflection;

namespace AggregatorPackage.Shared
{
    public class DependencyInjection
    {
        /// <summary>
        /// Builds the Container for injection using autofac
        /// </summary>
        /// <returns></returns>
        public static void BuildDependencyInjectionContainer()
        {
            
            ContainerBuilder builder = new ContainerBuilder();
           
            
            
            //Register anything we would like to have an injection with
            builder.RegisterType<SearchFunctions>().As<ISearchFunctions>();
            builder.RegisterType<MessageFormatter>().AsSelf();
            builder.RegisterInstance(EventAggregator.Instance).As<IEventAggregator>().SingleInstance();
            builder.RegisterType<AggregateMessage>().FindConstructorsWith(new NonPublicConstructorFinder(BindingFlags.NonPublic)).As<IAggregateMessage>();

            IContainer Container = builder.Build();
            //Any Constructor that needs to be resolved
            Container.Resolve<MessageFormatter>();
            Container.Resolve<IAggregateMessage>();
            Container.Resolve<ISearchFunctions>().Initialize();
            
        }

        
    }
    /// <summary>
    /// a class that works with internal types since autofac needs an override when jumping between public and internal
    /// </summary>
    internal class NonPublicConstructorFinder : DefaultConstructorFinder
    {
        public NonPublicConstructorFinder(BindingFlags nonPublic)
            : base(type => type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
        }
    }
}
