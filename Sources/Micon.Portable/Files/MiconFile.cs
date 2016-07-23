namespace Micon.Portable.Files
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Graphics;
    using Generation;
    using NGraphics;

    public class MiconPreview
    {
        public string Android { get; set; }

        public string Apple { get; set; }

        public string Windows { get; set; }

        public string WindowsSmall { get; set; }

        public string WindowsWide { get; set; }
    }

    public class MiconManifest
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }

    public class MiconIconSet
    {
        public MiconIconSet()
        {
            this.Manifests = new List<MiconManifest>();
            this.Icons = new List<Icon>();
        }
        public IEnumerable<MiconManifest> Manifests { get; set; }

        public IEnumerable<Icon> Icons { get; set; }
    }

    /// <summary>
    /// Represents the content of a file.
    /// </summary>
    public class MiconFile
    {
        public string LogoPath { get; set; }
        
        public double Scale { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public Color BackgroundColor { get; set; }

        [JsonConverter(typeof(ColorConverter))]
        public Color BackgroundEndColor { get; set; }
        
        public Shape BackgroundShape { get; set; }
        
        public bool BackgroundHasBorder { get; set; }
        
        public GradientMode GradientMode { get; set; }

        public MiconIconSet Icons { get; set; }

        public MiconPreview Preview { get; set; }
    }
}
