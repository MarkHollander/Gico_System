using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.EventStorage.Interfaces
{
    public interface IEventStorageDao
    {
        Task<long> Add(Message message);
        Task ChangsStatus(long stt, Event.StatusEnum status, string exception);
    }
}