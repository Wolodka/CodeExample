using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractCurrency : AppEntityConvertableAttribute<Entities.Contract, Guid>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ContractCurrency(Entities.Contract contract, EntityReferenceConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            Guid currencyGuid = TryToExtractCurrencyGuid();

            if (currencyGuid != Guid.Empty)
                _converter.Set(currencyGuid);
            else
                _converter.SetNull();
        }

        private Guid TryToExtractCurrencyGuid()
        {
            try
            {
                var currencyRepo = ExternalSystems.CurrencyRepository.Get();
                return currencyRepo.GetGuid(_appEntity.Currency);
            }
            catch(Exception exc)
            {
                _logger.Error(Exceptions.ERROR_WHEN_ASSIGNING_CURRENCY, _appEntity.Currency.ToString(), exc.Message);
            }

            return Guid.Empty;
        }

        public override void AssignToAppEntity()
        {
            Guid currencyGuid = _converter.GetOrDefault();
            if (currencyGuid != Guid.Empty)
            {
                try
                {
                    var currencyRepository = ExternalSystems.CurrencyRepository.Get();
                    _appEntity.Currency = currencyRepository.GetCurrency(currencyGuid);
                }
                catch (Exception exc)
                {
                    _logger.Error(Exceptions.ERROR_WHEN_ASSIGNING_CURRENCY, currencyGuid.ToString(), exc.Message);
                }
            }
        }
    }
}
