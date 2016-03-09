using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class StringConverter : AttributeConverter<string>
    {
        public StringConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }

        public override void Set(string value)
        {
            _crmEntity[_attributeKey] = value;
        }

        public override string Get()
        {
            return _crmEntity[_attributeKey].ToString();
        }

        protected override string GetDefaultValue()
        {
            return "";
        }
    }
}
