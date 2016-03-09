using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmSystemRepository : CrmSystem
    {
        public string GetSystemUrl()
        {
            QueryByAttribute qeParametr = new QueryByAttribute("ac_parametr");
            qeParametr.AddAttributeValue("ac_parametrtypecode", 4);
            qeParametr.ColumnSet = new ColumnSet("ac_value");

            var crmService = CrmService.GetProxy();
            var parametrs = crmService.RetrieveMultiple(qeParametr).Entities;
            foreach (var currentParament in parametrs)
            {
                if (currentParament.Contains("ac_value"))
                    return (string)currentParament["ac_value"];
            }

            return @"http://altus.asteros.ru/";
        }
    }
}
