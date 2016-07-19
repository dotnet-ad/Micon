namespace Micon.Portable.Generation
{
    using Graphics;

    /// <summary>
    /// Generator for icon bitmaps from logo and icon configuration.
    /// </summary>
    public interface IIconGenerator
    {
        /// <summary>
        /// Generates an icon from a logo and icon configuration.
        /// </summary>
        /// <param name="hdImage">The bitmap of the logo icon</param>
        /// <param name="icon">The icon configuration.</param>
        /// <returns>A new bitmap from specified configuration.</returns>
        IBitmap GenerateIcon(IBitmap hdImage, Icon icon);
    }
}
