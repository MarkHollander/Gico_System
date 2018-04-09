using System.Collections.Generic;
using Gico.Config;
using Newtonsoft.Json;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCodeCondition
    {
        public GiftCodeCondition(EnumDefine.GiftCodeConditionTypeEnum conditionType, object condition)
        {
            ConditionType = conditionType;
            Condition = condition;
        }

        public EnumDefine.GiftCodeConditionTypeEnum ConditionType { get; private set; }
        public object Condition { get; private set; }

    }
}