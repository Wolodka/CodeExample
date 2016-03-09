using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Cheating
{
    class ContragentCreator
    {
        public void Create(Contragent contragent)
        {
            var crmService = CrmService.GetProxy();
            Entity crmContragent = new ContragentConverter().ConvertToCrmEntity(contragent);

            crmService.Create(crmContragent);
        }
    }
}
