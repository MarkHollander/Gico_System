using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.EventStorage.Interfaces
{
    public interface IStorageDao 
    {
        Task Add(Message message);
        Task Add(Message message, Message messagesResult);
        Task Add(IEnumerable<Message> messages);
       
    }
}