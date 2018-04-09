using System;

namespace Gico.CQRS.Model.Interfaces
{
    public interface ICommand
    {
        string CommandId { get; }
        DateTime CreatedDateUtc { get; }
        int Version { get; }
    }
}