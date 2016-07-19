namespace Micon.Portable.Generation
{
    using Graphics;

    public class IconGenerator : IIconGenerator
    {
		public IconGenerator(IBitmapLoader loader)
		{
			// Dependencies
			this.loader = loader;
		}

		readonly IBitmapLoader loader;
     
		public IBitmap GenerateIcon(IBitmap hdImage, Icon icon)
		{
			var result = this.loader.Create(icon.Width, icon.Height);
            
            if (icon.BackgroundColor != null)
            {
                switch (icon.BackgroundShape)
                {
                    case Shape.RoundedRectangle:
                        result.DrawRectangle(icon.BackgroundColor, icon.BackgroundEndColor, icon.HasBorder, 10);
                        break;
                    case Shape.Circle:
                        result.DrawCircle(icon.BackgroundColor, icon.BackgroundEndColor, icon.HasBorder);
                        break;
                    default:
                        result.DrawRectangle(icon.BackgroundColor, icon.BackgroundEndColor, icon.HasBorder, 0);
                        break;
                }
            }

            if (hdImage != null)
            {
                var a = icon.ImageArea;

                var cx = a.X + a.Width / 2;
                var cy = a.Y + a.Height / 2;

                var area = new Rectangle(a.X, a.Y, a.Width, a.Height);
                area.Width = (int)(area.Width * icon.Scale);
                area.Height = (int)(area.Height * icon.Scale);
                area.X = cx - (area.Width / 2);
                area.Y = cy - (area.Height / 2);

                result.Draw(hdImage, area);
            }

			return result;
		}
	}
}