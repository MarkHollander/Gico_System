using Gico.Config;
using Gico.DataObject;

namespace Gico.CQRS.EventStorage.Implements
{
    public class StorageBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbEventStorageConnectionString.GetConfig();
        public class ProcName
        {
            public const string Event_Add = "Event_Add";
            public const string Event_ChangeStatus = "Event_ChangeStatus";
            public const string Command_Add = "Command_Add";
        }
    }
}