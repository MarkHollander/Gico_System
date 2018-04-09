using Gico.Config;
using Gico.ReadSystemModels;

namespace Gico.SystemDomains
{
    public class CustomerExternalLogin
    {
        public CustomerExternalLogin(EnumDefine.CutomerExternalLoginProviderEnum loginProvider, string providerKey, string providerDisplayName, string customerId, string info)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            ProviderDisplayName = providerDisplayName;
            CustomerId = customerId;
            Info = info;
        }

        public CustomerExternalLogin(RCustomerExternalLogin customerExternalLogin)
        {
            LoginProvider = customerExternalLogin.LoginProvider;
            ProviderKey = customerExternalLogin.ProviderKey;
            ProviderDisplayName = customerExternalLogin.ProviderDisplayName;
            CustomerId = customerExternalLogin.CustomerId;
            Info = customerExternalLogin.Info;
        }
        public EnumDefine.CutomerExternalLoginProviderEnum LoginProvider { get; private set; }
        public string ProviderKey { get; private set; }
        public string ProviderDisplayName { get; private set; }
        public string CustomerId { get; private set; }
        public string Info { get; private set; }

    }
}