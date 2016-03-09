using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.ContragentAttrs
{
    class ContragentName : AppEntityConvertableAttribute<Contragent, string>
    {
        public ContragentName(Contragent contract, StringConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.Name);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Name = _converter.GetOrDefault();
        }
    }
}
