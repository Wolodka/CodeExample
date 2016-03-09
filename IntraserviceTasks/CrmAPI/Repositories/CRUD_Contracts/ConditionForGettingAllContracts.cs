using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.Constants;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.CRUD_Contracts
{
    class ConditionForGettingAllContracts
    {
        private QueryExpression _query;

        public ConditionForGettingAllContracts(QueryExpression query)
        {
            _query = query;
        }

        public void AppendCondition()
        {
            var commandType = Config.Crm_GetContracts_CommandType;
            switch (commandType)
            {
                case Crm_GetContracts_Type.GetSingle:
                    AppendSingleContractCondition();
                    break;

                case Crm_GetContracts_Type.GetAll:
                default:
                    break;
            }
        }

        private void AppendSingleContractCondition()
        {
            _query.Criteria.AddCondition(
                                new ConditionExpression(ContractAttributes.INTRASERVICE_TASK_ID,
                                                        ConditionOperator.Equal,
                                                        Config.ID_OF_INTRASERVICE_TASK_TO_RETRIEVE));
        }

        private void AppendGetAllContractsCondition()
        { 
        
        }
    }
}
