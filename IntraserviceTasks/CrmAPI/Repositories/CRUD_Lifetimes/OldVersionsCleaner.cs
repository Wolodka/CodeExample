using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories.CRUD_Lifetimes
{
    public class OldVersionsCleaner : CrmLifetimesCleaner
    {
        private List<Guid> _guidsToDelete = new List<Guid>();
        
        public void CleanOldLifetimes()
        {
            GetOldLifetimesGuids();

            RemoveOldLifetimes();
        }

        public void GetOldLifetimesGuids()
        {
            var query = new QueryExpression();
            query.EntityName = LifetimesAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);
        
            query.Criteria.AddCondition(new ConditionExpression(LifetimesAttributes.VERSION,
                                                                ConditionOperator.NotEqual,
                                                                Configuration.Config.UpdateVersion.ToString()));
        
            var crmService = CrmService.GetProxy();
            var retrievedLifetimes = crmService.RetrieveMultiple(query).Entities;

            _guidsToDelete = retrievedLifetimes.Select(o => o.Id).ToList();
        }

        public void RemoveOldLifetimes()
        {
            var crmService = CrmService.GetProxy();

            foreach (var guid in _guidsToDelete)
                crmService.Delete(LifetimesAttributes.ENTITY_NAME, guid);
        }
    }
}
