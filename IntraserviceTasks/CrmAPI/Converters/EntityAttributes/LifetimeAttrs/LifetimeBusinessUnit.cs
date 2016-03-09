using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeBusinessUnit : AppEntityConvertableAttribute<TaskLifeTime, int>
    {
        public LifetimeBusinessUnit(TaskLifeTime lifetime, PicklistConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set((int)_appEntity.BusinessUnit);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.BusinessUnit = (BusinessUnit)_converter.GetOrDefault();
        }
    }
}
