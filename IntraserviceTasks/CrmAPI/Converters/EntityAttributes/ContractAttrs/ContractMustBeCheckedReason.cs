using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrmAPI.Converters.ContractAttrsAttrs;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractMustBeCheckedReason : AppEntityConvertableAttribute<Entities.Contract, string>
    {
        public ContractMustBeCheckedReason(Entities.Contract contract, StringConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.GetCheckReasons());
        }

        public override void AssignToAppEntity()
        {
            string crmReasonsString = _converter.GetOrDefault();
            _appEntity.CheckReasons = new MustBeCheckedReasonsParser(crmReasonsString).Parse();
        }
    }
}
