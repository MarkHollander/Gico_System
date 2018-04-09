using System;
using System.Collections.Generic;
using System.Text;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using static Gico.Config.EnumDefine;

namespace Gico.SystemDomains
{
    public class Manufacturer: BaseDomain, IVersioned
    {
        public Manufacturer()  {}
        public Manufacturer(int version)
        {
            Version = version;
        }



        #region Properties
        public new int Id { get; set; }
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        public string Logo { get; private set; }   
        
        
        public int Version { get; private set; }

        public new EnumDefine.StatusEnum Status { get; private set; }
        #endregion

        public void Add(ManufacturerManagementAddCommand command)
        {              
            Name = command.Name ?? string.Empty;            
            Description = command.Description ?? string.Empty;
            Logo = command.Logo ?? string.Empty;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
        }
        public void Change(ManufacturerManagementChangeCommand command)
        {
            Add(command);
            Id = command.Id;
            UpdatedDateUtc = command.UpdatedDateUtc;            
        }
    }
}
