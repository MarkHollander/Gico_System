using System.Threading.Tasks;
using Gico.FileDomains;

namespace Gico.FileDataObject.Interfaces
{
    public interface IFileRepository
    {
        Task Add(string connectionString, File file);
    }
}