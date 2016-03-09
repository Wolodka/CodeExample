using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeApprovalPerson : AppEntityConvertableAttribute<TaskLifeTime, Guid>
    {
        public LifetimeApprovalPerson(TaskLifeTime lifetime, EntityReferenceConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            CrmUserRepository userRep = ExternalSystems.CrmUserRepository.Get();

            Guid approvalUserGuid = userRep.GetGuidByLogin(_appEntity.ApprovalPerson.Login);
            if (approvalUserGuid != Guid.Empty)
                _converter.Set(approvalUserGuid);
        }

        public override void AssignToAppEntity()
        {
            Guid approvalPersonGuid = _converter.GetOrDefault();
            if (approvalPersonGuid != Guid.Empty)
            {
                CrmUserRepository userRep = ExternalSystems.CrmUserRepository.Get();
                _appEntity.ApprovalPerson = userRep.GetByGuid(approvalPersonGuid);
            }
        }
    }
}
