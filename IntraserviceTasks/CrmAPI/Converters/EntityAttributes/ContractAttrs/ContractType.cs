using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractType : AppEntityConvertableAttribute<Entities.Contract, int>
    {
        private BooleanConverter _ramkaCheckboxConverter;

        public ContractType(Entities.Contract contract, PicklistConverter converter, BooleanConverter ramkaCheckboxConverter)
            : base(contract, converter)
        {
            _ramkaCheckboxConverter = ramkaCheckboxConverter;
        }

        public override void AssignToCrmEntity()
        {
            if (_appEntity.Type == Entities.Enums.ContractType.Income
                || _appEntity.Type == Entities.Enums.ContractType.Outgo)
            {
                _converter.Set((int)_appEntity.Type);
                _ramkaCheckboxConverter.Set(false);
            }
            else if (_appEntity.Type == Entities.Enums.ContractType.Ramochnyi
                || _appEntity.Type == Entities.Enums.ContractType.OutgoSpecForRamochnyi)
            {
                _converter.SetNull();
                _ramkaCheckboxConverter.Set(true);
            }
        }

        public override void AssignToAppEntity()
        {
            int picklistValue = _converter.GetOrDefault();
            Entities.Enums.ContractType ctype = (Entities.Enums.ContractType)picklistValue;

            if (ctype == Entities.Enums.ContractType.Income
                || ctype == Entities.Enums.ContractType.Outgo)
            {
                _appEntity.Type = ctype;
            }
            else
            {
                _appEntity.Type = Entities.Enums.ContractType.Ramochnyi;
            }
        }
    }
}
