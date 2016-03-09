using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmUserRepositoryImpl : CrmUserRepository
    {
        public Person GetByGuid(Guid crmGuid)
        {
            var crmService = CrmService.GetProxy();
            var crmPerson = crmService.Retrieve(UserAttributes.ENTITY_NAME, crmGuid, new ColumnSet(true));

            return new UserConverter().ConvertToAppEntity(crmPerson);
        }

        public Guid GetGuidByLogin(string loginWithoutDomain)
        {
            string crmLogin = "bacint\\" + loginWithoutDomain;
            
            QueryExpression _query = new QueryExpression();
            _query.EntityName = UserAttributes.ENTITY_NAME;
            _query.ColumnSet = new ColumnSet(true);

            _query.Criteria.AddCondition(new ConditionExpression(UserAttributes.LOGIN,
                                                                        ConditionOperator.Like,
                                                                        crmLogin));

            var crmService = CrmService.GetProxy();
            var users = crmService.RetrieveMultiple(_query).Entities;

            if (users.Count > 0)
                return users[0].Id;
            else
                return Guid.Empty;
        }
    }
}
