using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.Configuration
{
    public class Config
    {
        public static string UpdateVersion = GenerateUpdateVerion();

        public static int ID_OF_INTRASERVICE_TASK_TO_RETRIEVE = -1;

        public static CrmEnvironmentType CrmType { get; set; }

        public static IS_GetLifetimes_Type IS_GetLifetimes_CommandType { get; set; }

        public static IS_GetTasks_Type IS_GetTasks_CommandType { get; set; }

        public static Crm_GetContracts_Type Crm_GetContracts_CommandType { get; set; }

        private static string GenerateUpdateVerion()
        {
            return DateTime.Now.ToString(Constants.LIFETIME_VERSION_FORMAT);
        }
    }
}
