using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.IS_API
{
    public class ISContractRetriever : ContractRetriever
    {
        public List<Contract> GetAll()
        {
            return new AllContractsRetriever(this).Retrieve();
        }

        public Contract Get(int id)
        {
            return new SingleContractRetriever(this).Retrieve(id);
        }
    }
}
