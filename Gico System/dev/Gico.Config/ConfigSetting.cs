using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gico.Common;
using Microsoft.Extensions.Configuration;

namespace Gico.Config
{
    public class ConfigSetting
    {

        public static void Init(IConfigurationProvider configurationProvider)
        {
            //ConfigurationProvider = configurationProvider;
            Configs = new Dictionary<ConfigSettingEnum, object>();
            var keys = Enum.GetValues(typeof(ConfigSettingEnum));
            foreach (ConfigSettingEnum key in keys)
            {
                if (!Configs.ContainsKey(key))
                {
                    string keyconfig = key.GetDisplayName();
                    if (configurationProvider.TryGet(keyconfig, out string valueConfig))
                    {
                        object value = null;
                        var order = (ConfigSettingTypeEnum)key.GetOrder();
                        switch (order)
                        {
                            case ConfigSettingTypeEnum.Bool:
                                value = valueConfig.AsInt() == 1;
                                break;
                            case ConfigSettingTypeEnum.Int:
                                value = valueConfig.AsInt();
                                break;
                            default:
                                value = valueConfig;
                                break;
                        }
                        Configs.Add(key, value);
                    }

                }
            }
        }

        public static IDictionary<ConfigSettingEnum, object> Configs;
        public static TimeSpan ReceiveCommandTimeout = new TimeSpan(0, 0, 10);
    }

    public enum ConfigSettingEnum
    {
        [Display(Name = "MessageSerializeType", Order = (int)ConfigSettingTypeEnum.Int)]
        MessageSerializeType = 2,
        [Display(Name = "JwtTokens:Key")]
        JwtTokensKey = 3,
        [Display(Name = "JwtTokens:Issuer")]
        JwtTokensIssuer = 4,
        [Display(Name = "JwtTokens:Audience")]
        JwtTokensAudience = 5,
        [Display(Name = "JwtTokens:Authority ")]
        JwtTokensAuthority,
        [Display(Name = "ConnectionStrings:DefaultConnection")]
        DefaultConnection = 6,
        [Display(Name = "IsTracking", Order = (int)ConfigSettingTypeEnum.Bool)]
        IsTracking = 7,
        [Display(Name = "BusMaxRetry", Order = (int)ConfigSettingTypeEnum.Int)]
        BusMaxRetry = 8,
        RabitMqHost = 9,
        RabitMqUserName,
        RabitMqPassword,
        RabitMqCommandExchangeName,
        RabitMqCommandRoutingKey,
        RabitMqCommandQueueName,
        RabitMqEventExchangeName,
        RabitMqEventRoutingKey,
        RabitMqEventQueueName,
        RabitMqEnvironment,
        [Display(Name = "ConnectionStrings:DbEventStorageConnectionString")]
        DbEventStorageConnectionString,
        [Display(Name = "ConnectionStrings:ShardingMasterConnectionString")]
        ShardingMasterConnectionString,
        [Display(Name = "ConnectionStrings:DbFileConnectionString")]
        DbFileConnectionString,
        [Display(Name = "ConnectionStrings:DbUserConnectionString")]
        DbUserConnectionString,
        [Display(Name = "ConnectionStrings:DbBackendConnectionString")]
        DbBackendConnectionString,
        [Display(Name = "ConnectionStrings:DbOrderConnectionString")]
        DbOrderConnectionString,
        [Display(Name = "ConnectionStrings:DbAddressConnectionString")]
        DbAddressConnectionString,
        [Display(Name = "ConnectionStrings:DbCommentConnectionString")]
        DbCommentConnectionString,
        [Display(Name = "ConnectionStrings:DbMarketingConnectionString")]
        DbMarketingConnectionString,
        [Display(Name = "ConnectionStrings:DbEmailOrSmsConnectionString")]
        DbEmailOrSmsConnectionString,
        LoginExpiresTime,
        [Display(Name = "WidthAndHeightAllow")]
        WidthAndHeightAllow,
        [Display(Name = "FilesExtension")]
        FilesExtension,
        [Display(Name = "UploadPath")]
        UploadPath,
        [Display(Name = "CdnPath")]
        CdnPath,
        [Display(Name = "CdnDomain")]
        CdnDomain,
        [Display(Name = "UploadDomain")]
        UploadDomain,
        [Display(Name = "EnableTracking")]
        EnableTracking,
        [Display(Name = "RedisHostIps")]
        RedisHostIps,
        [Display(Name = "RedisCacheDbId")]
        RedisCacheDbId,
        [Display(Name = "StoreId")]
        StoreId,
        [Display(Name = "InstanceName")]
        InstanceName,
        [Display(Name = "EsUrl")]
        EsUrl,
        [Display(Name = "FileStorageUrl")]
        FileStorageUrl,
        [Display(Name = "LanguageDefaultId")]
        LanguageDefaultId,
        [Display(Name = "CorsPolicyDomains")]
        CorsPolicyDomains,
        [Display(Name = "CookieDomain")]
        CookieDomain,
        [Display(Name = "TemplateDefault")]
        TemplateDefault,
        [Display(Name = "AuthFacebookUrl")]
        AuthFacebookUrl,
        [Display(Name = "AuthFacebookGetAccessTokenUrl")]
        AuthFacebookGetAccessTokenUrl,
        [Display(Name = "AuthFacebookAppid")]
        AuthFacebookAppid,
        [Display(Name = "AuthFacebookAppsecret")]
        AuthFacebookAppsecret,
        [Display(Name = "AuthGoogleClientid")]
        AuthGoogleClientid,
        [Display(Name = "AuthGoogleClientsecret")]
        AuthGoogleClientsecret,
        [Display(Name = "VerifyUrl")]
        VerifyUrl,
        [Display(Name = "AwsAccessKeyId")]
        AwsAccessKeyId,
        [Display(Name = "AwsSecretAccessKey")]
        AwsSecretAccessKey,
        [Display(Name = "AwsSenderAddress")]
        AwsSenderAddress,
        [Display(Name = "PageSizeDefault")]
        PageSizeDefault
    }

    public enum ConfigSettingTypeEnum
    {
        String = 0,
        Int = 1,
        Bool = 2
    }
    public enum SerializeTypeEnum
    {
        Json = 1,
        Protobuf = 2
    }

}
