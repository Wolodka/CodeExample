using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrmAPI.Converters.ContragentAttrs;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters
{
    class ContragentConverter : EntityConverter<Contragent>
    {
        protected override void FillAttributes()
        {
            _attributes = new List<ConvertableAttribute> 
            { 
                new ContragentName(_appEntity, new StringConverter(_crmEntity, ContragentAttributes.NAME)),
                new ContragentInn(_appEntity, new StringConverter(_crmEntity, ContragentAttributes.INN))
            };
        }

        protected override void FillCrmEntityName()
        {
            _crmEntityName = ContragentAttributes.ENTITY_NAME;
        }
    }
}
