using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractName : AppEntityConvertableAttribute<Entities.Contract, string>
    {
        public ContractName(Entities.Contract contract, StringConverter converter)
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
