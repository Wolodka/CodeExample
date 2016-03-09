using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractDate : AppEntityConvertableAttribute<Entities.Contract, DateTime>
    {
        public ContractDate(Entities.Contract contract, DateConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.ContractDate);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.ContractDate = _converter.GetOrDefault();
        }
    }
}
