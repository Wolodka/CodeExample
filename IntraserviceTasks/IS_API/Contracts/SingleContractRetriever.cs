using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.IS_API
{
    class SingleContractRetriever
    {
        private ISContractRetriever _parent;
        private Contract _contract;

        public SingleContractRetriever(ISContractRetriever parent)
        {
            _parent = parent;
        }

        public Contract Retrieve(int contractId)
        {
            _contract = new Contract();

            using (IntraServiceSqlDatabase db = new IntraServiceSqlDatabase())
            {
                string sqlCommandText = String.Format(
                    Constants.SQL_SELECT_GET_SINGLE_TASK_DATA, contractId.ToString());
                SqlDataReader dataReader = db.ExecuteCommandAndGetDataReader(sqlCommandText);

                while (dataReader.Read())
                {
                    _contract.Id = dataReader.GetInt32(2);
                    try
                    {
                        new ContractAttributeAppender(_contract, dataReader).Append();
                    }
                    catch
                    {

                    }
                }
            }

            CheckIfContractIsNull();

            SetName();

            return _contract;
        }

        private void CheckIfContractIsNull()
        {
            if (_contract.Id < 1)
                throw new MissingMemberException();
        }

        private void SetName()
        {
            using (IntraServiceSqlDatabase db = new IntraServiceSqlDatabase())
            {
                string sqlCommandText = String.Format(
                    Constants.SQL_SELECT_GET_SINGLE_TASK_NAME, _contract.Id.ToString());
                SqlDataReader dataReader = db.ExecuteCommandAndGetDataReader(sqlCommandText);

                while (dataReader.Read())
                    _contract.Name = dataReader.GetString(0);
            }
        }
    }
}
