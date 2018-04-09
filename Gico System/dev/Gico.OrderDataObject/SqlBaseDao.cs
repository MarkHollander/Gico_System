using Gico.DataObject;
using System;
using System.Data;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.Domains;
using Dapper;

namespace Gico.OrderDataObject
{
    public interface ISqlBaseDao
    {
        //Task Notify(BaseDomain baseDomain);
    }
    public class SqlBaseDao : BaseDao, ISqlBaseDao
    {
        //private readonly IEventSender _eventSender;

        //public SqlBaseDao(IEventSender eventSender)
        //{
        //    _eventSender = eventSender;
        //}

        public override string ConnectionString => ConfigSettingEnum.DbOrderConnectionString.GetConfig();

        //protected async Task<T> WithConnectionAndNotify<T>(string connectionString, Func<IDbConnection, Task<T>> getData, BaseDomain baseDomain)
        //{
        //    var result = await WithConnection(connectionString, getData);
        //    await Notify(baseDomain);
        //    return result;
        //}

        //protected async Task<T> WithConnectionAndNotify<T>(string connectionString, Func<IDbConnection, IDbTransaction, Task<T>> getData, BaseDomain[] baseDomains)
        //{
        //    var result = await WithConnection(connectionString, getData);
        //    foreach (var baseDomain in baseDomains)
        //    {
        //        await Notify(baseDomain);
        //    }
        //    return result;
        //}

        //public async Task Notify(BaseDomain baseDomain)
        //{
        //    await _eventSender.Notify(baseDomain.Events);
        //}
        public class ProcName
        {
            #region Cart

            public const string ShoppingCart_GetByCustomer = "ShoppingCart_GetByCustomer";
            public const string ShoppingCart_Add = "ShoppingCart_Add";
            public const string ShoppingCart_GetById = "ShoppingCart_GetById";
            public const string Menu_Change = "Menu_Change";
            public const string Menu_RemoveById = "Menu_RemoveById";

            #endregion

            #region ShoppingCartItem
            public const string ShoppingCartItem_GetByCartId = "ShoppingCartItem_GetByCartId";

            public const string ShoppingCartItem_Add = "ShoppingCartItem_Add";
            public const string ShoppingCartItem_ChangeStatus = "ShoppingCartItem_ChangeStatus";
            public const string ShoppingCartItem_ChangeStatusWithCountItem = "ShoppingCartItem_ChangeStatusWithCountItem";
            public const string ShoppingCart_ChangeVersion = "ShoppingCart_ChangeVersion";
            public const string ShoppingCartItem_Remove = "ShoppingCartItem_Remove";
            #endregion

            #region ShoppingCartItemDetail

            public const string ShoppingCartItemDetail_Add = "ShoppingCartItemDetail_Add";
            public const string ShoppingCartItemDetail_Remove = "ShoppingCartItemDetail_Remove";
            public const string ShoppingCartItemDetail_GetByCartId = "ShoppingCartItemDetail_GetByCartId";

            #endregion

            #region GiftCodeCampaign
            public const string GiftCodeCampaign_GetById = "GiftCodeCampaign_GetById";
            public const string GiftCodeCampaign_Gets = "GiftCodeCampaign_Gets";
            public const string GiftCodeCampaign_Add = "GiftCodeCampaign_Add";
            public const string GiftCodeCampaign_Change = "GiftCodeCampaign_Change";

            #endregion
        }
    }
}
