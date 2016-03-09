using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.UserAttrs
{
    class UserActiveInCrm : AppEntityConvertableAttribute<Person, bool>
    {
        public UserActiveInCrm(Person contract, BooleanConverter inversionConverter)
            : base(contract, inversionConverter)
        { }

        public override void AssignToCrmEntity()
        {
            throw new NotImplementedException();
        }

        public override void AssignToAppEntity()
        {
            _appEntity.ActiveInCrm = !_converter.GetOrDefault();
        }
    }
}
