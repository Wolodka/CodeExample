using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractIntraserviceId : AppEntityConvertableAttribute<Entities.Contract, int>
    {
        public ContractIntraserviceId(Entities.Contract contract, IntConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            _converter.Set(_appEntity.Id);
        }

        public override void AssignToAppEntity()
        {
            _appEntity.Id = _converter.GetOrDefault();
        }
    }
}
