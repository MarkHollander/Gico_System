using System;
using RabbitMQ.Client.Impl;

namespace Gico.SystemCommands
{
    public class CategoryAddCommand:CQRS.Model.Implements.Command
    {
        public CategoryAddCommand()
        {

        }
        public CategoryAddCommand(int version) : base(version)
        {

        }
        public string Name { get; set; }
       
        public string ParentId { get; set; }
       
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public long Status { get; set; }

        public string LanguageId { get; set; }

        public string StoreId { get; set; }

        public string Code { get; set; }
        public string CreatedUid { get; set; }

    }
}
