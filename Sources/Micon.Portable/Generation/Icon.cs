using System;

namespace Micon.Portable.Generation
{

	public class Icon
	{
		public Icon(string name, int size) : this(name, size, size)
		{
		}

		public Icon(string name, int width, int height)
        {
            this.Name = name;
			this.Width = width;
			this.Height = height;

			var areaSize = Math.Min(width, height);
			this.ImageArea = new Rectangle((width / 2) - (areaSize / 2), (height / 2) - (areaSize / 2), areaSize, areaSize);
		}

		public string Name { get; private set; }

		public int Width { get; set; }

		public int Height { get; set; }
        
        public Rectangle ImageArea { get; }

		public Color BackgroundColor { get; set; }

        public Color BackgroundEndColor { get; set; }

        public Shape BackgroundShape { get; set; }
        
        public bool HasBorder { get; set; }
    }
}

