using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Gico.Common;

namespace Gico.Config
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var configName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            if (string.IsNullOrEmpty(configName))
            {
                return enumValue.ToString();
            }
            return configName;
        }
        public static int GetOrder(this Enum enumValue)
        {
            var orderConfig = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetOrder().GetValueOrDefault();
            return orderConfig.GetValueOrDefault(0);
        }
        public static string GetConfig(this ConfigSettingEnum enumValue)
        {
            if (ConfigSetting.Configs.TryGetValue(enumValue, out var configValue))
                return configValue.ToString();
            return string.Empty;
        }
        //public static T GetConfig<T>(this ConfigSettingEnum enumValue)
        //{
        //    if (ConfigSetting.Configs.TryGetValue(enumValue, out var configValue))
        //    {
        //        var t = typeof(T);
        //        if (t == typeof(int))
        //        {
        //            return (T)(configValue.AsInt());
        //        }
        //    }
        //    return default(T);
        //}

    }
}