using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.CrmAPI.Converters.EntityAttributes.LifetimeAttrs
{
    class LifetimeContract : AppEntityConvertableAttribute<TaskLifeTime, Guid>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LifetimeContract(TaskLifeTime lifetime, EntityReferenceConverter converter)
            : base(lifetime, converter)
        { }

        public override void AssignToCrmEntity()
        {
            try
            {
                AssignCrmContractToCrmLifetime();
            }
            catch (Exception exc)
            {
                //todo uncomment later, когда будет настроена синхронизация по контрактам
                /*_logger.Error(Exceptions.ERROR_WHEN_ASSIGNING_TASKLIFETIME_TO_CONTRACT,
                    _appEntity.TaskId.ToString(),
                    _appEntity.Id.ToString(),
                    exc.Message);*/
            }
        }

        private void AssignCrmContractToCrmLifetime()
        {
            var crmContractsRepo = ExternalSystems.CrmContractsRepository.Get();
            var crmContract = crmContractsRepo.GetContractByIntraserviceIdOrException(_appEntity.TaskId);

            if (crmContract.CrmGuid != Guid.Empty)
                _converter.Set(crmContract.CrmGuid);
        }

        public override void AssignToAppEntity()
        {
        }
    }
}
