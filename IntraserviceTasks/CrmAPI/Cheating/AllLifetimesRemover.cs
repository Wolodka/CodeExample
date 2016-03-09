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
    public class AllLifetimesRemover
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void RemoveAll()
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = LifetimesAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            var crmService = CrmService.GetProxy();
            _logger.Info("Started CRM query to find all lifetimes...");
            var lifetimes = crmService.RetrieveMultiple(query).Entities;
            _logger.Info("Done");

            foreach (var lt in lifetimes)
            {
                Guid deletedGuid = lt.Id;
                crmService.Delete(LifetimesAttributes.ENTITY_NAME, deletedGuid);
                _logger.Info("Deleted {0}", deletedGuid);
            }
        }
    }
}
