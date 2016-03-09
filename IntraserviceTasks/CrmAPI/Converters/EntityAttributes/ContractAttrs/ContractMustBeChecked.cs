using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractMustBeChecked : AppEntityConvertableAttribute<Entities.Contract, bool>
    {
        public ContractMustBeChecked(Entities.Contract contract, BooleanConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.MustBeChecked);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.MustBeChecked = _converter.GetOrDefault();
        }
    }
}
