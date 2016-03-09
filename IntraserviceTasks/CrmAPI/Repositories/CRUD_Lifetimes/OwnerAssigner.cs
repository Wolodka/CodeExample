using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.CRUD_Lifetimes
{
    class OwnerAssigner
    {
        public void Assign(Guid lifetimeId, Guid ownerId)
        { 
            AssignRequest request = new AssignRequest
            {
                Assignee = new EntityReference(UserAttributes.ENTITY_NAME, ownerId),
                Target = new EntityReference(LifetimesAttributes.ENTITY_NAME, lifetimeId)
            };

            CrmService.GetProxy().Execute(request);
        }
    }
}
