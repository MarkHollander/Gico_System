using System;
using Gico.Common;
using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Model.Implements
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            CommandId = Common.Common.GenerateGuid();
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
        }
        public Command(int version):this()
        {
            Version = version;
        }
        public Command(int version,int shardId) : this()
        {
            Version = version;
            ShardId = shardId;
        }
        public string CommandId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public int Version { get; set; }
        public int ShardId { get; set; }

    }
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
        }
        public StatusEnum Status { get; set; }
        public string Message { get; set; }
        public string ObjectId { get; set; }
        public string MessageId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string ResourceName { get; set; }
        public bool IsSucess => Status == StatusEnum.Sucess;

        public enum StatusEnum
        {
            Sucess = 1,
            Fail = 2
        }
    }

}