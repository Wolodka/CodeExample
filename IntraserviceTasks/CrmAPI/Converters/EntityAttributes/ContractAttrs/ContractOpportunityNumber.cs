using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractOpportunityNumber : AppEntityConvertableAttribute<Entities.Contract, string>
    {
        public ContractOpportunityNumber(Entities.Contract contract, StringConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.OpportunityNumber.ToString());
        }

        public override void AssignToAppEntity()
        {
            string oppNumberString = _converter.GetOrDefault();
            int oppNumber;
            if (int.TryParse(oppNumberString, out oppNumber))
                _appEntity.OpportunityNumber = oppNumber;
        }
    }
}
