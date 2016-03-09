using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class IntConverter : AttributeConverter<int>
    {
        public IntConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }

        public override void Set(int value)
        {
            _crmEntity[_attributeKey] = value;
        }

        public override int Get()
        {
            return (int)_crmEntity[_attributeKey];
        }

        protected override int GetDefaultValue()
        {
            return 0;
        }
    }
}
