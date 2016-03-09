using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrs
{
    class ContractContragent : AppEntityConvertableAttribute<Entities.Contract, Guid>
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ContractContragent(Entities.Contract contract, EntityReferenceConverter converter)
            : base(contract, converter)
        { }

        public override void AssignToCrmEntity()
        {
            try
            {
                var contragentRepo = ExternalSystems.ContragentRepository.Get();

                var contragents = contragentRepo
                    .GetByCompositeInnString(_appEntity.Contragent.INN);

                if (contragents.Count > 0)
                    _converter.Set(contragents.First().CrmGuid);
                else
                    _converter.SetNull();
            }
            catch (Exception exc)
            {
                _logger.Error(Exceptions.ERROR_WHEN_ASSIGNING_CONTRAGENT,
                    _appEntity.Contragent.INN, _appEntity.Contragent.Name, exc.Message);
            }
        }

        public override void AssignToAppEntity()
        {
            Guid contragentGuid = _converter.GetOrDefault();
            if (contragentGuid != Guid.Empty)
            {
                try
                {
                    var contragentRepo = ExternalSystems.ContragentRepository.Get();
                    _appEntity.Contragent = contragentRepo.Get(contragentGuid);
                }
                catch (Exception exc)
                {
                    _logger.Error(Exceptions.ERROR_WHEN_ASSIGNING_CONTRAGENT, "<o>", "<o>", exc.Message);
                }
            }
        }
    }
}
