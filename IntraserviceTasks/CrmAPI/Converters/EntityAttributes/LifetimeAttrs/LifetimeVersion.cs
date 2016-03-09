using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeVersion : AppEntityConvertableAttribute<TaskLifeTime, string>
    {
        public LifetimeVersion(TaskLifeTime lifetime, StringConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.Version);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Version = _converter.GetOrDefault();
        }
    }
}
