using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrmAPI.Converters.ContractAttrs;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;
using Microsoft.Xrm.Sdk;
using NLog;

namespace IntraserviceTasks.CrmAPI.Converters
{
    class ContractConverter : EntityConverter<Contract>
    {
        protected override void FillAttributes()
        {
            _attributes = new List<ConvertableAttribute>()
            {
                new ContractIntraserviceId(_appEntity, new IntConverter(_crmEntity, ContractAttributes.INTRASERVICE_TASK_ID)),

                new ContractAttrs.ContractType(_appEntity, 
                                                new PicklistConverter(_crmEntity, ContractAttributes.TYPE),
                                                new BooleanConverter(_crmEntity, ContractAttributes.IS_FRAME)),
                
                new ContractName(_appEntity, new StringConverter(_crmEntity, ContractAttributes.NAME)),
                
                new ContractNumber(_appEntity, new StringConverter(_crmEntity, ContractAttributes.NUMBER)),
                
                new ContractOpportunityNumber(_appEntity, new StringConverter(_crmEntity, ContractAttributes.OPPORTUNITY_NUMBER)),
                
                new ContractMustBeChecked(_appEntity, new BooleanConverter(_crmEntity, ContractAttributes.MUST_BE_CHECKED)),
                
                new ContractMustBeCheckedReason(_appEntity, new StringConverter(_crmEntity, ContractAttributes.MUST_BE_CHECKED_REASON)),
                
                new ContractDate(_appEntity, new DateConverter(_crmEntity, ContractAttributes.CONTRACT_DATE)),
                
                new ContractTotalAmount(_appEntity, new MoneyConverter(_crmEntity, ContractAttributes.NET_AMOUNT)),
                
                new ContractCurrency(_appEntity, new EntityReferenceConverter(  _crmEntity,
                                                                                ContractAttributes.CURRENCY,
                                                                                CurrencyAttributes.ENTITY_NAME)),

                new ContractContragent(_appEntity, new EntityReferenceConverter(_crmEntity,
                                                                                ContractAttributes.CONTRAGENT,
                                                                                ContragentAttributes.ENTITY_NAME)),

                new ContractZkNumber(_appEntity, new StringConverter(_crmEntity, ContractAttributes.ZK_NUMBER))
            };
        }

        protected override void FillCrmEntityName()
        {
            _crmEntityName = ContractAttributes.ENTITY_NAME;
        }
    }
}
