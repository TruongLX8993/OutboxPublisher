using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publisher.Model;

namespace Publisher
{
    public interface IOutBoxEventRepository
    {
        Task<IList<OutboxEvent>> GetNewEvents();
        Task UpdateEventStatus(IList<string> eventIds,OutBoxEventStatus status);
        Task UpdateEventStatus(string eventId,OutBoxEventStatus status);
    }
}