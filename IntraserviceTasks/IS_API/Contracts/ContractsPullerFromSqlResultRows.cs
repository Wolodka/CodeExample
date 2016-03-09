using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.IS_API.Contracts
{
    class ContractsPullerFromSqlResultRows
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private List<Contract> _contracts = new List<Contract>();
        private SqlDataReader _dataReader;
        private Contract _currentContract = null;
        private int _currentRowTaskId = 0;

        public ContractsPullerFromSqlResultRows(SqlDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public List<Contract> Retrieve()
        {
            while (_dataReader.Read())
            {
                AssignCurrentRowTaskId();

                if (IsNewTaskInfo())
                {
                    TryToSaveCurrentContract();
                    CreateNewCurrentContract();
                }

                UpdateCurrentContract();
            }
            SaveCurrentContract();

            return _contracts;
        }

        private void AssignCurrentRowTaskId()
        {
            _currentRowTaskId = GetTaskIdFromDbRow();
        }

        private int GetTaskIdFromDbRow()
        {
            return _dataReader.GetInt32(2);
        }

        private bool IsNewTaskInfo()
        {
            return IsFirstContract() ||
                    !AreEqualCurrentRowTaskId_and_CurrentContractId();
        }

        private bool IsFirstContract()
        {
            return _currentContract == null;
        }

        private bool AreEqualCurrentRowTaskId_and_CurrentContractId()
        {
            return _currentRowTaskId == _currentContract.Id;
        }

        private void TryToSaveCurrentContract()
        {
            if (!IsFirstContract())
                SaveCurrentContract();
        }

        private void SaveCurrentContract()
        {
            _contracts.Add(_currentContract);
            _logger.Info(LogMessages.RETRIEVED_SINGLE_CONTRACT_WITH_ID, _currentContract.Id.ToString());
        }

        private void CreateNewCurrentContract()
        {
            _currentContract = new Contract();
            _currentContract.Id = GetTaskIdFromDbRow();
        }

        private void UpdateCurrentContract()
        {
            try
            {
                new ContractAttributeAppender(_currentContract, _dataReader).Append();
            }
            catch
            {

            }
        }
    }
}
