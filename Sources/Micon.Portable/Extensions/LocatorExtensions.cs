using System;

namespace Micon
{
	public static class LocatorExtensions
	{
		public static void Register<T>(this Splat.IMutableDependencyResolver resolver, Func<T> factory, string contract = null)
		{
			resolver.Register(() => factory(), typeof(T), contract);
		}


		public static T GetService<T>(this Splat.IDependencyResolver resolver,string contract = null)
		{
			return (T)resolver.GetService(typeof(T),contract);
		}
	}
}

