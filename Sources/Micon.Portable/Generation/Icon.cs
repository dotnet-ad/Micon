namespace Micon.Portable.Generation
{
    using Graphics;
    using NGraphics;
    using System;

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

        public Icon(Icon icon) : this(icon.Name, icon.Width, icon.Height)
        {
            Scale = icon.Scale;
            ImageArea = new Rect(icon.ImageArea.X, icon.ImageArea.Y, icon.ImageArea.Width, icon.ImageArea.Height);
            BackgroundColor = icon.BackgroundColor;
            BackgroundShape = icon.BackgroundShape;
            HasBorder = icon.HasBorder;
        }

        public Icon(string name, int size) : this(name, size, size)
        {
        }

        public string Name { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }
        
        private Rect imageArea;

        public Rect ImageArea
        {
            get
            {
                if(imageArea.Width == 0 && imageArea.Height == 0)
                {
                    var areaSize = Math.Min(this.Width, this.Height);
                    return new Rect((this.Width / 2) - (areaSize / 2), (this.Height / 2) - (areaSize / 2), areaSize, areaSize);
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
    }
}

