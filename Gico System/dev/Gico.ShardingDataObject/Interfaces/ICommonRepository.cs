using System.Threading.Tasks;

namespace Gico.ShardingDataObject.Interfaces
{
    public interface ICommonRepository
    {
        Task<long> GetNextValueForSequence(string pathName);
        Task CreateSequence(string pathName);
    }
}