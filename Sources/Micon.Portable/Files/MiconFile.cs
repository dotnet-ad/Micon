using Micon.Portable.Generation;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Micon.Portable.Files
{
    public class MiconPreview
    {
        public string Android { get; set; }

        public string Apple { get; set; }

        public string Windows { get; set; }

        public string WindowsSmall { get; set; }

        public string WindowsWide { get; set; }
    }

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

        public IEnumerable<Icon> Icons { get; set; }

        public MiconPreview Preview { get; set; }
    }
}
