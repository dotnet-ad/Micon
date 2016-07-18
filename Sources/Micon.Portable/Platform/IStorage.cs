namespace Micon.Portable.Platform
{
    using System.Threading.Tasks;

    public interface IStorage
    {
        Task Save(string path, string content);

        Task<string> Load(string path);
    }
}
