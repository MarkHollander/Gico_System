using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.Models.Request;


namespace Gico.SystemModels.Request
{
    public class ManufacturerManagementAddOrChangeRequest: BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; } 
    }
}
