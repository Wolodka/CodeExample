using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.AttrConverters
{
    class EntityReferenceConverter : AttributeConverter<Guid>
    {
        string _referencedEntityName;

        public EntityReferenceConverter(Entity crmEntity, string attributeKey, string referencedEntityName)
            : base(crmEntity, attributeKey)
        {
            _referencedEntityName = referencedEntityName;
        }

        public override void Set(Guid value)
        {
            _crmEntity[_attributeKey] = new EntityReference(_referencedEntityName, value);
        }

        public override Guid Get()
        {
            return _crmEntity.GetAttributeValue<EntityReference>(_attributeKey).Id;
        }

        protected override Guid GetDefaultValue()
        {
            return Guid.Empty;
        }
    }
}
