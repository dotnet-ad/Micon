namespace Micon
{
    using Portable.Platform;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public static class StorageExtensions
    {
        public static Task Save<T>(this IStorage storage, string path, T content)
        {
            return storage.Save(path, JsonConvert.SerializeObject(content));
        }

        public static async Task<T> Load<T>(this IStorage storage, string path)
        {
            var json = await storage.Load(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
