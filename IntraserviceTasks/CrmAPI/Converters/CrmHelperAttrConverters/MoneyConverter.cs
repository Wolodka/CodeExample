using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class MoneyConverter: AttributeConverter<decimal>
    {
        public MoneyConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }

        public override void Set(decimal value)
        {
            _crmEntity[_attributeKey] = new Money(value);
        }

        public override decimal Get()
        {
            return _crmEntity.GetAttributeValue<Money>(_attributeKey).Value;
        }

        protected override decimal GetDefaultValue()
        {
            return 0;
        }
    }
}
