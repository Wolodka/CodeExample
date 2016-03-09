using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    abstract class AttributeConverter<T>
    {
        protected Entity _crmEntity;
        protected string _attributeKey;

        public AttributeConverter(Entity entity, string attributeKey)
        {
            _crmEntity = entity;
            _attributeKey = attributeKey;
        }

        public abstract void Set(T value);
        public abstract T Get();

        public T GetOrDefault()
        {
            if (_crmEntity.Attributes.Contains(_attributeKey))
                return Get();

            return GetDefaultValue();
        }

        protected abstract T GetDefaultValue();

        public void SetNull()
        {
            _crmEntity[_attributeKey] = null;
        }
    }
}
