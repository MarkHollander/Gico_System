using Gico.Config;
using Gico.DataObject;
using System;

namespace Gico.CommentDataObject
{
    public class SqlBaseDao:BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbCommentConnectionString.GetConfig();

        public class ProcName
        {
            #region Comment
            public const string Comment_Get = "Comment_Get";


            #endregion
        }
    }
}
