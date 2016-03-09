using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;
using IntraserviceTasks.Res;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs;


namespace IntraserviceTasks.CrmAPI.Converters
{
    class LifetimeConverter : EntityConverter<TaskLifeTime>
    {
        protected override void FillAttributes()
        {
            _attributes = new List<ConvertableAttribute>()
            {
                new LifetimeIntraserviceId(_appEntity, new IntConverter(_crmEntity, LifetimesAttributes.INTRASERVICE_ID)),
                
                new LifetimeIntraserviceTaskId(_appEntity, new IntConverter(_crmEntity, LifetimesAttributes.INTRASERVICE_TASK_ID)),
                
                new LifetimeContract(_appEntity, new EntityReferenceConverter(_crmEntity, LifetimesAttributes.CONTRACT, ContractAttributes.ENTITY_NAME)),

                new LifetimeStartOfApproval(_appEntity, new DateConverter(_crmEntity, LifetimesAttributes.START_OF_APPROVAL)),
                
                new LifetimeApprovalPerson(_appEntity, new EntityReferenceConverter(_crmEntity, LifetimesAttributes.APPROVAL_PERSON, UserAttributes.ENTITY_NAME)),
                
                new LifetimeApprovalDeadline(_appEntity, new DateConverter(_crmEntity, LifetimesAttributes.APPROVAL_DEADLINE)),
                
                new LifetimeApprovalStatus(_appEntity, new PicklistConverter(_crmEntity, LifetimesAttributes.APPROVAL_STATUS)),
                
                new LifetimeVersion(_appEntity, new StringConverter(_crmEntity, LifetimesAttributes.VERSION)),

                new LifetimeBusinessUnit(_appEntity, new PicklistConverter(_crmEntity, LifetimesAttributes.BUSINESS_UNIT))
            };
        }

        protected override void FillCrmEntityName()
        {
            _crmEntityName = LifetimesAttributes.ENTITY_NAME;
        }
    }
}
