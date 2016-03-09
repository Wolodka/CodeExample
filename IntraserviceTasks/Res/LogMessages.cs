using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Res
{
    class LogMessages
    {
        public const string RETRIEVED_SINGLE_CONTRACT_WITH_ID = "Retrieved contract with id {0}";
        
        public const string START_RETRIEVING_CONTRACT_NAMES = "Started retrieving of contract names...";
        public const string FINISHED_RETRIEVING_CONTRACT_NAMES = "Finished retrieving of contract names";
        public const string ASSIGNED_NAMES_TO_CONTRACTS = "Assigned names to contracts";

        public const string STARTED_EXECUTING_GET_SQL_COMMAND = "Started executing of SQL command";
        public const string FINISHED_EXECUTING_GET_SQL_COMMAND = "Finished executing of SQL command";

        public const string FINISHED_SYNC_OF_SINGLE_LIFETIME_WITH_ID = "Synced ltid {0}";
        public const string FINISHED_SYNC_OF_SINGLE_CONTRACT_WITH_ID = "Synced tid {0}";

        public const string RETRIEVE_CRM_CONTRACTS_STARTED = "Started retrieving of crm contracts...";
        public const string RETRIEVED_CRM_CONTRACT = "Retrieved Crm Contract {0}";
    }
}
