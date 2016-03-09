using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeApprovalDeadline : AppEntityConvertableAttribute<TaskLifeTime, DateTime>
    {
        public LifetimeApprovalDeadline(TaskLifeTime lifetime, DateConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.ApprovalDeadline);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.ApprovalDeadline = _converter.GetOrDefault();
        }
    }
}
