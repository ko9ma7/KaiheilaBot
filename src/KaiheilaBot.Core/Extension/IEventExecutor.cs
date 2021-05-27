using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension
{
    internal interface IEventExecutor<T> where T : class
    {
        Task ExecuteInner(T e);
    }
}
