namespace Micon.Portable.Data
{
    using Micon.Portable.Generation;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class Icons
    {
        public const string iOS = "iOS";

        public const string Android = "Android";

        public const string Windows = "Windows";

        public static IEnumerable<Icon> Load(string name)
        {
            var assembly = typeof(Icons).GetTypeInfo().Assembly;
            var resourceName = $"{assembly.GetName().Name}.Data.{name}.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var result =  JsonConvert.DeserializeObject<IEnumerable<Icon>>(json);
                foreach (var item in result)
                {
                    item.Name = $"{name}/{item.Name}";
                }
                return result;
            }
        }
    }
}
