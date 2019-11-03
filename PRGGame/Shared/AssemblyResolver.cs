using System;
using System.Linq;
using System.Reflection;

namespace AggregatorPackage.Shared
{
    internal class FunctionsAssemblyResolver
    {
        /// <summary>
        /// Was Built to fix a problem with Azure Functions May still be required later down on the road
        /// </summary>
        /// <returns></returns>
        public static string RedirectAssembly()
        {
            var list = AppDomain.CurrentDomain.GetAssemblies()
                .OrderByDescending(a => a.FullName)
                .Select(a => a.FullName)
                .ToList();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            return"";
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var requestedAssembly = new AssemblyName(args.Name);
            Assembly assembly = null;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            try
            {
                assembly = Assembly.Load(requestedAssembly.Name);
            }
            catch (Exception ex)
            {
                //Doesn't currently do anything
                Console.Write(ex.Message);
            }
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            return assembly;
        }

    }
}
