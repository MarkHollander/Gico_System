namespace Gico.Resilience.Http
{
    public class HttpClientConnection
    {
        private static volatile HttpClientConnection _instance;
        public static readonly object SyncLock = new object();
        public static HttpClientConnection Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new HttpClientConnection();
                        }
                    }
                }

                return _instance;
            }
        }
        private HttpClientConnection()
        {
            _httpClient = Create();
        }
        public System.Net.Http.HttpClient GetConnection => _httpClient ?? (_httpClient = Create());

        private System.Net.Http.HttpClient _httpClient;

        private System.Net.Http.HttpClient Create()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36");
            return httpClient;
        }

    }
}