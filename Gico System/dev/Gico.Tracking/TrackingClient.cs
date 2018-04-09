using System;
using System.Diagnostics;
using Gico.Common;
using Microsoft.Extensions.Logging;
using Gico.Config;

namespace Gico.Tracking
{
    public class TrackingClient
    {
        private readonly ILogger<TrackingClient> _logger;
        private TrackingClient(string functionName, object input, ILogger<TrackingClient> logger)
        {
            if (!ConfigSettingEnum.EnableTracking.GetConfig().AsBool())
            {
                return;
            }
            Data = input;
            _logger = logger;
            FunctionName = functionName;
            BeginDate = Common.Extensions.GetCurrentDateUtc();
        }
        public Stopwatch Stopwatch { get; private set; }
        public object Data { get; }
        public string FunctionName { get; set; }
        public DateTime BeginDate { get; set; }

        public static TrackingClient StartNew(string functionName, object input, ILogger<TrackingClient> logger)
        {
            var trackingClient = new TrackingClient(functionName, input, logger);
            trackingClient.Start();
            return trackingClient;
        }

        public void Start()
        {
            if (!ConfigSettingEnum.EnableTracking.GetConfig().AsBool())
            {
                return;
            }
            Stopwatch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            if (!ConfigSettingEnum.EnableTracking.GetConfig().AsBool())
            {
                return;
            }
            Stopwatch.Stop();
            Log();
        }

        private void Log()
        {
            TrackingObjectDetail logObjectDetail = new TrackingObjectDetail(FunctionName, TimeProcess, Data, BeginDate, Common.Extensions.GetCurrentDateUtc());
            _logger.LogWarning(Common.Serialize.JsonSerializeObject(logObjectDetail));
        }

        public long TimeProcess => Stopwatch?.ElapsedMilliseconds ?? 0;

        public static string GetLogScript(DateTime date, int pageIndex, int pageSize, out string urlPath)
        {
            urlPath = $"{GetLogName(date)}/{LogType}";
            string query =
                $"{{\"query\":{{\"bool\":{{\"must\":[{{\"match_all\":{{}}}}]}}}},\"from\":{pageIndex * pageSize},\"size\":{pageSize}}}";
            return query;
        }

        public static string GetLogName(DateTime dateTime)
        {
            return $"logstash-{dateTime.Year}.{dateTime.Month}.{dateTime.Day}";
        }

        public const string LogType = "myCustomLogEventType";
    }
}
