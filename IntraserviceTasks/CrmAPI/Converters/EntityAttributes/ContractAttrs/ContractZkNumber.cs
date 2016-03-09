using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractZkNumber : AppEntityConvertableAttribute<Contract, string>
    {
        public ContractZkNumber(Entities.Contract contract, StringConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.ZkNumber);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.ZkNumber = _converter.GetOrDefault();
        }
    }
}
