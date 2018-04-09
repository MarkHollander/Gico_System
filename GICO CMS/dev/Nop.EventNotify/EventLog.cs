using System;

namespace Nop.EventNotify
{
    public class EventLog
    {
        public long Id { get; set; }
        public string Data { get; set; }
        public string DataType { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string Message { get; set; }
    }
}