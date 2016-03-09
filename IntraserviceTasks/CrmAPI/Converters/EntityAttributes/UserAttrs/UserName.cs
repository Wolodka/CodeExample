using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.UserAttrs
{
    class UserName : AppEntityConvertableAttribute<Person, string>
    {
        public UserName(Person person, StringConverter converter)
            : base(person, converter)
        { }

        public override void AssignToCrmEntity()
        {
            throw new NotImplementedException();
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Name = _converter.GetOrDefault();
        }
    }
}
