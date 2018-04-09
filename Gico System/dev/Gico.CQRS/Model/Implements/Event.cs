using System;
using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Model.Implements
{
    public abstract class Event : IEvent
    {
        protected Event()
        {
            EventId = Common.Common.GenerateGuid();
        }
        public virtual string EventId { get; }

        public enum StatusEnum
        {
            New = 0,
            Success = 1,
            Fail = -1,
            Retry = 2
        }
    }
}