using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrmAPI.CRUD_Contracts;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk.Client;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmContractsRepositoryImpl : CrmContractsRepository
    {
        public void CreateContract(Contract contract)
        {
            new ContractsCreator().Create(contract);
        }

        public void UpdateContract(Guid contractId, Contract contract)
        {
            contract.CrmGuid = contractId;
            new ContractsUpdater().Update(contract);
        }

        public void MarkContractAsMustBeChecked(int intraserviceId, List<MustCheckContractReason> checkReasons)
        {
            new ContractsUpdater().MarkAsMustBeChecked(intraserviceId, checkReasons);
        }

        public Contract GetContractByIntraserviceIdOrException(int intraserviceId)
        {
            return new ContractsGetter().GetContractByIntraserviceIdOrException(intraserviceId);
        }

        public List<Contract> GetAll()
        {
            return new ContractsGetter().GetAll();
        }
        
        public Contract Get(int id)
        {
            return new ContractsGetter().GetByISTaskIdOrException(id);
        }
    }
}
