using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeStartOfApproval : AppEntityConvertableAttribute<TaskLifeTime, DateTime>
    {
        public LifetimeStartOfApproval(TaskLifeTime lifetime, DateConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.StartOfApproval);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.StartOfApproval = _converter.GetOrDefault();
        }
    }
}
