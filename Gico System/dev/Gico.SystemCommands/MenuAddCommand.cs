using System;
using Gico.Config;
using RabbitMQ.Client.Impl;

namespace Gico.SystemCommands
{
    public class MenuAddCommand : CQRS.Model.Implements.Command
    {
        public MenuAddCommand()
        {

        }
        public MenuAddCommand(int version) : base(version)
        {

        }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public EnumDefine.MenuPositionEnum Position { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public long Status { get; set; }
        public string LanguageId { get; set; }
        public string Condition { get; set; }
        public string StoreId { get; set; }
        public int Priority { get; set; }


    }
}
