using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractTotalAmount : AppEntityConvertableAttribute<Entities.Contract, decimal>
    {
        public ContractTotalAmount(Entities.Contract contract, MoneyConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.TotalAmount);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.TotalAmount = _converter.GetOrDefault();
        }
    }
}
