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
    class LifeTimeCreator
    {
        public void Create(TaskLifeTime lifetime)
        {
            Entity crmLifetime = new LifetimeConverter().ConvertToCrmEntity(lifetime);
            
            Guid newLifetimeGuid = CrmService.GetProxy().Create(crmLifetime);

            /*var ownerId = new EntityReferenceConverter(crmLifetime, 
                            LifetimesAttributes.APPROVAL_PERSON, 
                            UserAttributes.ENTITY_NAME).GetOrDefault();
            if (ownerId != Guid.Empty)
                new OwnerAssigner().Assign(newLifetimeGuid, ownerId);*/
        }
    }
}
