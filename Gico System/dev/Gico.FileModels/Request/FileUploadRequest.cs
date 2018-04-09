namespace Gico.FileModels.Request
{
    public class FileUploadRequest
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string CreatedUid { get; set; }
    }
}