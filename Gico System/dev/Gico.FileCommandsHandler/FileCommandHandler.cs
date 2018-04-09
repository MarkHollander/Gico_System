using Gico.CQRS.Service.Interfaces;
using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.FileDomains;
using Gico.FileCommands;
using Gico.FileDataObject.Interfaces;
using Gico.ShardingConfigService.Interfaces;

namespace Gico.FileCommandsHandler
{
    public class FileCommandHandler : ICommandHandler<ImageAddCommand, ICommandResult>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IShardingService _shardingService;
        private EnumDefine.ShardGroupEnum ShardGroup = EnumDefine.ShardGroupEnum.File;
        public FileCommandHandler(IFileRepository fileRepository, IShardingService shardingService)
        {
            _fileRepository = fileRepository;
            _shardingService = shardingService;
        }
        public async Task<ICommandResult> Handle(ImageAddCommand mesage)
        {
            try
            {
                var shard = await _shardingService.GetCurrentWriteShardByYear(ShardGroup);
                File file = new File(shard.Id);
                file.Add(mesage.FileName, mesage.Extentsion, File.TypeEnum.Image, mesage.FilePath, mesage.CreatedUid);
                await _fileRepository.Add(shard.ConnectionString, file);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = file.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
        }
    }
}
