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

		public async Task<IBitmap[]> Generate(string hdImagePath, string backgroundImage, string outputFolder, Generation.SystemIcons system)
		{
			var bitmap = await this.loader.LoadAsync(hdImagePath);
			var background = await this.loader.LoadAsync(backgroundImage);
			return await this.Generate(bitmap,background, outputFolder, system);
		}

		public Task<IBitmap[]> Generate(IBitmap hdImage, IBitmap backgroundImage, string outputFolder, Generation.SystemIcons system)
		{
			var tasks = system.Icons.Select((icon) => GenerateIcon(hdImage,backgroundImage,outputFolder,system,icon));
			return Task.WhenAll(tasks);
		}

		public async Task<IBitmap> GenerateIcon(IBitmap hdImage, IBitmap backgroundImage, string outputFolder, Generation.SystemIcons system, Icon icon)
		{
			outputFolder = outputFolder.TrimEnd(new char[] { '/', '\\' });
			var path = $"{outputFolder}/{system.Name}/{icon.Name}";
			var result = await this.GenerateIcon(path, hdImage, backgroundImage, icon);
			await result.Save();
			return result;
		}

		public async Task<IBitmap> GenerateIcon(string path, IBitmap hdImage, IBitmap backgroundImage, Icon icon)
		{
			var result = await this.loader.Create(path, icon.Width, icon.Height);

			if (icon.HasBackground)
			{
				result.Draw(backgroundImage, result.Bounds);
			}

			result.Draw(hdImage, icon.ImageArea);
			return result;
		}
	}
}