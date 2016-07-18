using Micon.Portable.Generation;
using Newtonsoft.Json;

namespace Micon.Portable.Files
{
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
    }
}
