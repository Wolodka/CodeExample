using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.CRUD_Lifetimes
{
    class LifeTimeUpdater
    {
        public void Update(TaskLifeTime lifetime)
        {
            lifetime.CrmGuid = new LifetimesGetter().GetCrmGuid(lifetime);
            Entity crmLifetime = new LifetimeConverter().ConvertToCrmEntity(lifetime);

            CrmService.GetProxy().Update(crmLifetime);

            /*var ownerId = new EntityReferenceConverter(crmLifetime,
                            LifetimesAttributes.APPROVAL_PERSON,
                            UserAttributes.ENTITY_NAME).GetOrDefault();
            if (ownerId != Guid.Empty)
                new OwnerAssigner().Assign(crmLifetime.Id, ownerId);*/
        }
    }
}
