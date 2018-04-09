using Gico.CQRS.Model.Implements;
using System;

namespace Gico.FileCommands
{
    public class ImageAddCommand : Command
    {
        public ImageAddCommand()
        {
        }

        public ImageAddCommand(int version) : base(version)
        {
        }

        public string FileName { get; set; }
        public string Extentsion { get; set; }
        public string FilePath { get; set; }
        public string CreatedUid { get; set; }
        
    }
}
