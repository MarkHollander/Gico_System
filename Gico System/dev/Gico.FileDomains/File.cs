using System;
using Gico.Domains;

namespace Gico.FileDomains
{
    public class File : BaseDomain
    {
        public File(int shardId)
        {
            this.ShardId = shardId;
        }
        public void Add(string fileName, string extentsion, TypeEnum type, string filePath, string createdUid)
        {
            Id = Common.Common.GenerateGuid();
            ParentId = string.Empty;
            FileName = fileName;
            Extension = extentsion;
            Type = (int)type;
            FilePath = filePath;
            Info = string.Empty;
            CreatedDateUtc = DateTime.UtcNow;
            CreatedUid = createdUid;
            UpdatedDateUtc = DateTime.UtcNow;
            UpdatedUid = createdUid;
        }

        public string ParentId { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }
        public int Type { get; private set; }
        public string FilePath { get; private set; }
        public string Info { get; private set; }

        public object InfoObject
        {
            get
            {
                switch (Type)
                {
                    case (int)TypeEnum.Image:
                        return Common.Serialize.JsonDeserializeObject<ImageInfo>(Info);
                    case (int)TypeEnum.Video:
                        return Common.Serialize.JsonDeserializeObject<VideoInfo>(Info);
                    default:
                        return null;
                }
            }
            private set => Info = Common.Serialize.JsonSerializeObject(value);
        }

        public enum TypeEnum
        {
            Image,
            Video
        }
        public class ImageInfo
        {
            public int With { get; set; }
            public int Height { get; set; }
        }
        public class VideoInfo
        {
            public string BitRate { get; set; }
            public int Time { get; set; }
        }


    }
}
