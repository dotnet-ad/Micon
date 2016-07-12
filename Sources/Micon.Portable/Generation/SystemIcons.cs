using System;
using System.Collections.Generic;

namespace Micon.Portable.Generation
{
	public class SystemIcons
	{
		public SystemIcons()
		{
			this.Icons = new List<Icon>();
		}

		public string Name { get; set; }

		public List<Icon> Icons { get; set; }
	}
}

