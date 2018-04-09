using System;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.Model.Interfaces
{
    public interface ICommandResult
    {
        CommandResult.StatusEnum Status { get; set; }
        string Message { get; set; }
        string ObjectId { get; set; }
        string MessageId { get; set; }
        DateTime CreatedDateUtc { get; set; }
        string ResourceName { get; set; }
    }
}