using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk.Query;
using NLog;

namespace IntraserviceTasks.CrmAPI.CRUD_Contracts
{
    class ContractsGetter
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public List<Contract> GetAll()
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = ContractAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            new ConditionForGettingAllContracts(query).AppendCondition();
            
            var crmService = CrmService.GetProxy();
            var crmContracts = crmService.RetrieveMultiple(query);

            List<Contract> contracts = new List<Contract>();
            _logger.Info(Res.LogMessages.RETRIEVE_CRM_CONTRACTS_STARTED);
            foreach (var c in crmContracts.Entities)
            {
                Contract contract = new ContractConverter().ConvertToAppEntity(c);
                contracts.Add(contract);
                _logger.Info(Res.LogMessages.RETRIEVED_CRM_CONTRACT, contract.Id);
            }

            return contracts;
        }

        public Contract GetContractByIntraserviceIdOrException(int intraserviceId)
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = ContractAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression(ContractAttributes.INTRASERVICE_TASK_ID,
                                                                    ConditionOperator.Equal,
                                                                    intraserviceId));

            var crmService = CrmService.GetProxy();
            var contracts = crmService.RetrieveMultiple(query);

            return new ContractConverter().ConvertToAppEntity(contracts[0]);
        }

        public Contract GetByISTaskIdOrException(int taskId)
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = ContractAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression(ContractAttributes.INTRASERVICE_TASK_ID,
                                                                    ConditionOperator.Equal,
                                                                    taskId));

            var crmService = CrmService.GetProxy();
            var contracts = crmService.RetrieveMultiple(query);

            return new ContractConverter().ConvertToAppEntity(contracts[0]);
        }
    }
}
