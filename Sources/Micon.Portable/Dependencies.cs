using System;
using Micon.Portable.Generation;
using Autofac;
using Micon.Portable;

namespace Micon
{
	public static class Dependencies
	{
		public static void RegisterShared(this ContainerBuilder container)
        {
            //Services
            container.RegisterType<IconGenerator>().As<IIconGenerator>();

            //ViewModels
            container.RegisterType<HomeViewModel>();
        }
	}
}

