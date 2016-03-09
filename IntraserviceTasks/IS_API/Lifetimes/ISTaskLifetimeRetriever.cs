using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.IS_API
{
    public class ISTaskLifetimeRetriever : TaskLifeTimesRetriever
    {
        public List<TaskLifeTime> Get()
        {
            string connectionString = ConfigurationManager.AppSettings[Constants.IS_DATABASE_KEY];

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = SqlCommandSelectLifeTimes.GetCommand(Config.IS_GetLifetimes_CommandType);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            reader = cmd.ExecuteReader();

            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>();
            while (reader.Read())
            {
                TaskLifeTime lifetime = new TaskLifeTime();

                lifetime.Id = reader.GetInt32(Constants.INDEX_OF_LIFETIME_ATTR_APPROVAL_ID);

                lifetime.TaskId = reader.GetInt32(Constants.INDEX_OF_LIFETIME_ATTR_TASK_ID);

                lifetime.StartOfApproval = reader.GetDateTime(Constants.INDEX_OF_LIFETIME_ATTR_START_OF_APPROVAL);

                if (!reader.IsDBNull(Constants.INDEX_OF_LIFETIME_ATTR_APPROVAL_DATE))
                    lifetime.ApprovalDate = reader.GetDateTime(Constants.INDEX_OF_LIFETIME_ATTR_APPROVAL_DATE);

                lifetime.TaskStatus = StringToEnumParser<Entities.Enums.TaskStatus>.Parse
                    (reader.GetString(Constants.INDEX_OF_LIFETIME_ATTR_TASK_STATUS), Constants.TaskStatuses);

                lifetime.ApprovalPerson.Login = reader.GetString(Constants.INDEX_OF_LIFETIME_ATTR_APPROVAL_PERSON_LOGIN);

                lifetime.ApprovalStatus = StringToEnumParser<ApprovalStatus>.Parse
                    (reader.GetString(Constants.INDEX_OF_LIFETIME_ATTR_APPROVAL_STATUS), Constants.ApprovalStatuses);

                lifetime.BusinessUnit = StringToEnumParser<BusinessUnit>.Parse
                    (reader.GetString(Constants.INDEX_OF_LIFETIME_ATTR_BUSINESS_UNIT), Constants.BusinessUnits);

                lifetimes.Add(lifetime);
            }

            sqlConnection.Close();

            return lifetimes;
        }

        public List<TaskLifeTime> GetOverdued()
        {
            throw new NotImplementedException();
        }
    }
}
