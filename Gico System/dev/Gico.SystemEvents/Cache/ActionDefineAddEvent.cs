using Gico.Events;

namespace Gico.SystemEvents.Cache
{
    public class ActionDefineAddEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }
}