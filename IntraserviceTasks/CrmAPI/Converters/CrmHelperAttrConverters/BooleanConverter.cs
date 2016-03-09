using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class BooleanConverter: AttributeConverter<bool>
    {
        public BooleanConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }
        
        public override void Set(bool value)
        {
            _crmEntity[_attributeKey] = value;
        }

        public override bool Get()
        {
            return (bool)_crmEntity[_attributeKey];
        }

        protected override bool GetDefaultValue()
        {
            return false;
        }
    }
}
