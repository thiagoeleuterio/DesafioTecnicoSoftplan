using Microsoft.Extensions.DependencyInjection;
using System;

namespace Calculo.Integration.Tests.Helpers
{
    public class NativeInjectorBootStrapper
    {
        private static ServiceCollection _serviceCollection = new ServiceCollection();
        internal static IServiceProvider _container => _serviceCollection.BuildServiceProvider();

        public static T GetInstance<T>()
        {
            var instance = (T)_container.GetService(typeof(T));
            return instance;
        }



        public static ServiceCollection RegisterAll()
        {
            //DbContext
            return _serviceCollection;
        }
    }
}
