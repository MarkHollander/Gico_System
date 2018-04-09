using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class ProductChangeCommand : ProductAddCommand
    {
        public ProductChangeCommand()
        {
        }
        public ProductChangeCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }

        public DateTime UpdatedDateUtc => this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}
