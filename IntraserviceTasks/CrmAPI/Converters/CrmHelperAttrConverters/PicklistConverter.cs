using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class PicklistConverter: AttributeConverter<int>
    {
        public PicklistConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }

        public override void Set(int value)
        {
            _crmEntity[_attributeKey] = new OptionSetValue(value);
        }

        public override int Get()
        {
            return _crmEntity.GetAttributeValue<OptionSetValue>(_attributeKey).Value;
        }

        protected override int GetDefaultValue()
        {
            return -1;
        }
    }
}
