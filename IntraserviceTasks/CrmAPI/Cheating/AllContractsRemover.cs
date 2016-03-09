using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using Microsoft.Xrm.Sdk.Query;
using NLog;

namespace IntraserviceTasks.CrmAPI.Cheating
{
    public class AllContractsRemover
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void RemoveAll()
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = ContractAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression
                (   ContractAttributes.MODIFIED_ON,
                    ConditionOperator.GreaterThan,
                    new object[] 
                    { 
                        new DateTime(2015, 12, 23, 23, 59, 0).ToString("yyyy-MM-ddThh:mm:ss") 
                    }
                ));

            var crmService = CrmService.GetProxy();
            _logger.Info("Started CRM query to find all contracts...");
            var contracts = crmService.RetrieveMultiple(query).Entities;
            _logger.Info("Done");

            foreach (var contract in contracts)
            { 
                Guid deletedGuid = contract.Id;
                crmService.Delete(ContractAttributes.ENTITY_NAME, deletedGuid);
                _logger.Info("Deleted {0}", deletedGuid);
            }
        }
    }
}
