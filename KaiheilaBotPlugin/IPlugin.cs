using KaiheilaBot.Models;
using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    public interface IPlugin
    {
        public EventType HandleType { get; }
        public Task Handle(MessageEventArgs eventArgs);
    }
}
