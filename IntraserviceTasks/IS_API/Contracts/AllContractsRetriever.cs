using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.Entities;
using IntraserviceTasks.IS_API.Contracts;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.IS_API
{
    class AllContractsRetriever
    {
        private ISContractRetriever _parent;
        private List<Contract> _contracts;
        private SqlDataReader _dataReader;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AllContractsRetriever(ISContractRetriever parent)
        {
            _parent = parent;
        }

        public List<Contract> Retrieve()
        {
            using (IntraServiceSqlDatabase db = new IntraServiceSqlDatabase())
            {
                CreateDataReaderAndExecuteSqlCommand(db);

                _contracts = new ContractsPullerFromSqlResultRows(_dataReader).Retrieve();   
            }

            AssignContractNames();

            return _contracts;
        }

        private void CreateDataReaderAndExecuteSqlCommand(IntraServiceSqlDatabase db)
        {
            _logger.Info(LogMessages.STARTED_EXECUTING_GET_SQL_COMMAND);

            string sqlCommandText = SqlCommandSelectTasks.GetCommand(Config.IS_GetTasks_CommandType);
            _dataReader = db.ExecuteCommandAndGetDataReader(sqlCommandText);

            _logger.Info(LogMessages.FINISHED_EXECUTING_GET_SQL_COMMAND);
        }

        private int GetCurrentRowTaskId()
        {
            return _dataReader.GetInt32(2); ;
        }

        private void AssignContractNames()
        {
            Dictionary<int, string> tasksNames = new Dictionary<int, string>();
            _logger.Info(LogMessages.START_RETRIEVING_CONTRACT_NAMES);
            using (IntraServiceSqlDatabase db = new IntraServiceSqlDatabase())
            {
                SqlDataReader _dataReader = db.ExecuteCommandAndGetDataReader(
                    Constants.SQL_SELECT_GET_TASKS_NAMES);

                while (_dataReader.Read())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    tasksNames[id] = name;
                }
            }
            _logger.Info(LogMessages.FINISHED_RETRIEVING_CONTRACT_NAMES);

            foreach (var contract in _contracts)
                contract.Name = tasksNames[contract.Id];

            _logger.Info(LogMessages.ASSIGNED_NAMES_TO_CONTRACTS);
        }
    }
}
