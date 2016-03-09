using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrmAPI.Converters.EntityAttributes.UserAttrs;
using IntraserviceTasks.CrmAPI.Converters.UserAttrs;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters
{
    class UserConverter : EntityConverter<Person>
    {
        protected override void FillAttributes()
        {
            _attributes = new List<ConvertableAttribute> 
            { 
                new UserLogin(_appEntity, new StringConverter(_crmEntity, UserAttributes.LOGIN)),
                new UserActiveInCrm(_appEntity, new BooleanConverter(_crmEntity, UserAttributes.IS_DISABLED)),
                new UserName(_appEntity, new StringConverter(_crmEntity, UserAttributes.NAME))
            };
        }

        protected override void FillCrmEntityName()
        {
            _crmEntityName = UserAttributes.ENTITY_NAME;
        }
    }
}
