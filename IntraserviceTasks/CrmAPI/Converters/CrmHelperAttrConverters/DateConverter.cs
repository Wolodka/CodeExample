using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class DateConverter: AttributeConverter<DateTime>
    {
        private DateTime _minCrmValue = new DateTime(2000, 1, 1);

        public DateConverter(Entity crmEntity, string attributeKey)
            : base(crmEntity, attributeKey)
        { }

        public override void Set(DateTime value)
        {
            if (value > _minCrmValue)
                _crmEntity[_attributeKey] = value;
            else
                SetNull();
        }

        public override DateTime Get()
        {
            DateTime date = (DateTime)_crmEntity[_attributeKey];
            if (date < _minCrmValue)
                return _minCrmValue;
            else
                return date;
        }

        protected override DateTime GetDefaultValue()
        {
            return _minCrmValue;
        }
    }
}
