using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeIntraserviceId : AppEntityConvertableAttribute<TaskLifeTime, int>
    {
        public LifetimeIntraserviceId(TaskLifeTime lifetime, IntConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.Id);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Id = _converter.GetOrDefault();
        }
    }
}
