using System;
using Micon.Portable.Generation;

namespace Micon.Portable
{
	public abstract class Dependencies
	{
		public void Register()
		{
			var locator = Splat.Locator.CurrentMutable;
			this.RegisterPlatform(locator);
			this.RegisterShared(locator);
		}

		private void RegisterShared(Splat.IMutableDependencyResolver locator)
		{
			locator.Register(() => new IconGenerator());
		}

		protected abstract void RegisterPlatform(Splat.IMutableDependencyResolver locator);

	}
}

