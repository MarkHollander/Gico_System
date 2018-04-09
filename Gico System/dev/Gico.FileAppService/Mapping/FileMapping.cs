using Gico.Config;
using Gico.FileCommands;
using Gico.FileModels.Request;

namespace Gico.FileAppService.Mapping
{
    public static class FileMapping
    {
        public static ImageAddCommand ToCommand(this ImageAddModel model)
        {
            if (model == null) return null;
            return new ImageAddCommand(SystemDefine.DefaultVersion)
            {
                Extentsion = model.Extension,
                FileName = model.FileName,
                FilePath = model.FilePath,
                CreatedUid = model.CreatedUid,

            };
        }
    }
}