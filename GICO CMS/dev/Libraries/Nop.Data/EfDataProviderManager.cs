using Nop.Core;
using Nop.Core.Data;

namespace Nop.Data
{
    /// <summary>
    /// Entity Framework data provider manager
    /// </summary>
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">Data settings</param>
        public EfDataProviderManager():base()
        {
        }

        /// <summary>
        /// Load data provider
        /// </summary>
        /// <returns>Data provider</returns>
        public override IDataProvider LoadDataProvider()
        {
            return new SqlServerDataProvider();
            //var providerName = Settings.DataProvider;
            //if (string.IsNullOrWhiteSpace(providerName))
            //    throw new NopException("Data Settings doesn't contain a providerName");

            //switch (providerName.ToLowerInvariant())
            //{
            //    case "sqlserver":
            //        return new SqlServerDataProvider();
            //    case "sqlce":
            //        return new SqlCeDataProvider();
            //    default:
            //        throw new NopException($"Not supported dataprovider name: {providerName}");
            //}
        }
    }
}
