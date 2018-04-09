using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class LanguageAddCommand : Command
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public string LastIpAddress { get; set; }
    }
    public class LanguageChangeCommand : LanguageAddCommand
    {
        public int Id { get; set; }
    }
}