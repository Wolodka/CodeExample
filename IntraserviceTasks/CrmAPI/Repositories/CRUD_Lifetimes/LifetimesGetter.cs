using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.CRUD_Lifetimes
{
    class LifetimesGetter
    {
        private QueryExpression _query;
        private DataCollection<Entity> _retrievedLifetimes;

        public bool LifetimeExists(TaskLifeTime lifetime)
        {
            GetLifetimeEntitiesByApprovalId(lifetime);

            return _retrievedLifetimes.Count > 0;
        }

        private void GetLifetimeEntitiesByApprovalId(TaskLifeTime lifetime)
        {
            GenerateQueryToFindAll();
            AddConditionToFindByApprovalId(lifetime);
            RetrieveByQuery();
        }

        private void GenerateQueryToFindAll()
        {
            _query = new QueryExpression();
            _query.EntityName = LifetimesAttributes.ENTITY_NAME;
            _query.ColumnSet = new ColumnSet(true);
        }

        private void AddConditionToFindByApprovalId(TaskLifeTime lifetime)
        {
            _query.Criteria.AddCondition(new ConditionExpression(LifetimesAttributes.INTRASERVICE_ID,
                                                                        ConditionOperator.Equal,
                                                                        lifetime.Id));
        }
        
        private void RetrieveByQuery()
        {
            var crmService = CrmService.GetProxy();
            _retrievedLifetimes = crmService.RetrieveMultiple(_query).Entities;
        }

        public Guid GetCrmGuid(TaskLifeTime lifetime)
        {
            GetLifetimeEntitiesByApprovalId(lifetime);

            return _retrievedLifetimes[0].Id;
        }

        public List<TaskLifeTime> GetAll()
        {
            GenerateQueryToFindAll();
            RetrieveByQuery();

            return ConvertRetrievedEntitiesToTaskLifeTime();
        }

        private List<TaskLifeTime> ConvertRetrievedEntitiesToTaskLifeTime()
        {
            return _retrievedLifetimes.Select(o => new LifetimeConverter().ConvertToAppEntity(o)).ToList();
        }

        public List<TaskLifeTime> GetOverdued()
        {
            GenerateQueryToFindAll();
            AddConditionToFindOverdued();
            RetrieveByQuery();

            return ConvertRetrievedEntitiesToTaskLifeTime();
        }

        private void AddConditionToFindOverdued()
        {
            _query.Criteria.AddCondition(new ConditionExpression(LifetimesAttributes.APPROVAL_DEADLINE,
                ConditionOperator.LessEqual,
                new object[] { DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss") } ));
            _query.Criteria.FilterOperator = LogicalOperator.And;
            _query.Criteria.AddCondition(new ConditionExpression(LifetimesAttributes.APPROVAL_PERSON,
                                                                        ConditionOperator.NotNull));
        }
    }
}
