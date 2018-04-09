using System.Threading.Tasks;
using Gico.Config;

namespace Gico.SendMail
{
    public interface ISendMailClient
    {
        Task<bool> Send(string fromMail, string toMail, string title, string content, Ref<string> response);
    }
}
