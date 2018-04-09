using System;

namespace Gico.Tracking
{
    public class TrackingObjectDetail
    {
        public TrackingObjectDetail()
        {

        }

        public TrackingObjectDetail(string functionName, long timeProcess, object data,DateTime beginDate,DateTime endDate)
        {
            FunctionName = functionName;
            TimeProcess = timeProcess;
            Data = data;
            BeginDate = beginDate;
            EndDate = endDate;
        }
        public string FunctionName { get; set; }
        public long TimeProcess { get; set; }
        public object Data { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}