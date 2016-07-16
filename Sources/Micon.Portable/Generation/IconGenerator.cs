using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micon.Portable.Bitmaps;

namespace Micon.Portable.Generation
{
	public class IconGenerator
	{
		public IconGenerator(IBitmapLoader loader = null)
		{
			// Dependencies
			var locator = Splat.Locator.Current;
			this.loader = loader ?? (IBitmapLoader)locator.GetService(typeof(IBitmapLoader));
		}

		readonly IBitmapLoader loader;

		public async Task<IBitmap[]> Generate(string hdImagePath, string outputFolder, Generation.SystemIcons system)
		{
			var bitmap = await this.loader.LoadAsync(hdImagePath);
			return await this.Generate(bitmap, outputFolder, system);
		}

		public Task<IBitmap[]> Generate(IBitmap hdImage, string outputFolder, Generation.SystemIcons system)
		{
			var tasks = system.Icons.Select((icon) => GenerateIcon(hdImage,outputFolder,system,icon));
			return Task.WhenAll(tasks);
		}

		public async Task<IBitmap> GenerateIcon(IBitmap hdImage, string outputFolder, Generation.SystemIcons system, Icon icon)
		{
			outputFolder = outputFolder.TrimEnd(new char[] { '/', '\\' });
			var path = $"{outputFolder}/{system.Name}/{icon.Name}";
			var result = await this.GenerateIcon(path, hdImage,  icon);
			await result.Save();
			return result;
		}

		public async Task<IBitmap> GenerateIcon(string path, IBitmap hdImage, Icon icon)
		{
			var result = await this.loader.Create(path, icon.Width, icon.Height);
            
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
            
            if(hdImage != null)
			    result.Draw(hdImage, icon.ImageArea);

			return result;
		}
	}
}