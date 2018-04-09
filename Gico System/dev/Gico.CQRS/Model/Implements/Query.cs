using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Model.Implements
{
    public abstract class Query : IQuery
    {
        public abstract object Result { get; set; }
    }
}