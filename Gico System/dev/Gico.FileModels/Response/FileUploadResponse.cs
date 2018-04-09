using Gico.Models.Response;

namespace Gico.FileModels.Response
{
    public class FileUploadResponse : BaseResponse
    {
        public string FullUrl => HostName + Path + "/" + Name;

        public string Name { get; set; }
        public string Path { get; set; }
        public string HostName { get; set; }
        public new bool Status { get; set; }
        public string Message { get; set; }
    }
}