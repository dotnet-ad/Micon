using System;

namespace Micon.Portable.Generation
{

	public class Icon
	{
        public Icon()
        {
            this.Scale = 1;
        }

		public Icon(string name, int width, int height) : this()
        {
            this.Name = name;
			this.Width = width;
			this.Height = height;
        }

        public Icon(string name, int size) : this(name, size, size)
        {
        }

        public string Name { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }
        
        private Rectangle imageArea;

        public Rectangle ImageArea
        {
            get
            {
                if(imageArea == null)
                {
                    var areaSize = Math.Min(this.Width, this.Height);
                    return new Rectangle((this.Width / 2) - (areaSize / 2), (this.Height / 2) - (areaSize / 2), areaSize, areaSize);
                }
                return imageArea;
            }
            set { imageArea = value; }
        }


        public double Scale { get; set; }

        public Color BackgroundColor { get; set; }

        public Color BackgroundEndColor { get; set; }

        public Shape BackgroundShape { get; set; }
        
        public bool HasBorder { get; set; }

        public Icon Copy()
        {
            return new Icon(this.Name, this.Width, this.Height)
            {
                Scale = Scale,
                ImageArea = new Rectangle(this.ImageArea.X, this.ImageArea.Y, this.ImageArea.Width, this.ImageArea.Height),
                BackgroundColor = BackgroundColor,
                BackgroundShape = BackgroundShape,
                HasBorder = HasBorder,
            };
        }
    }
}

