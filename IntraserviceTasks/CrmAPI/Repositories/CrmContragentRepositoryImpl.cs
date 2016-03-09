using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmContragentRepositoryImpl : CrmContragentRepository
    {
        public List<Contragent> GetByCompositeInnString(string InnString)
        {
            List<string> possibleInnNumbers = new InnParser(InnString).Parse();

            List<Contragent> contragents = new List<Contragent>();
            foreach (var innNumber in possibleInnNumbers)
                contragents = GetByExactInnNumber(innNumber);

            return contragents;
        }

        private List<Contragent> GetByExactInnNumber(string INN)
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = ContragentAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression(ContragentAttributes.INN,
                                                                    ConditionOperator.Like,
                                                                    INN));

            var crmService = CrmService.GetProxy();
            var contragents = crmService.RetrieveMultiple(query).Entities;

            ContragentConverter converter = new ContragentConverter();

            return contragents.Select(o => converter.ConvertToAppEntity(o)).ToList();
        }

        public Contragent Get(Guid crmGuid)
        {
            var crmService = CrmService.GetProxy();
            var crmContragent = crmService.Retrieve(ContragentAttributes.ENTITY_NAME, crmGuid, new ColumnSet(true));

            return new ContragentConverter().ConvertToAppEntity(crmContragent);
        }
    }
}
