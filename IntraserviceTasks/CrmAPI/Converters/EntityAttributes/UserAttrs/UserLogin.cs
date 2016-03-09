using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.UserAttrs
{
    class UserLogin : AppEntityConvertableAttribute<Person, string>
    {
        public UserLogin(Person contract, StringConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            throw new NotImplementedException();
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Login = _converter.GetOrDefault();
        }
    }
}
