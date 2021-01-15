using System.Threading.Tasks;

namespace DDM.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}