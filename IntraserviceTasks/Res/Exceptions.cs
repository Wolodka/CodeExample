using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Res
{
    class Exceptions
    {
        public const string REST_TIME_CALC_WRONG_SEQUENCE = "Wrong sequence of arguments";
        public const string OVERDUE_CHECKER_UNKNOWN_TYPE = "Unknown value of taskLifeTime type";

        public const string CRM_ENTITY_DOES_NOT_CONTAIN_ATTRIBUTE = "Entity {0} with id {1} does not contain attribute {2}";

        public const string EXTERNAL_REPOSITORY_WAS_NOT_INITIALIZE = "{0}: Repository was not initialized";
        public const string INTRASERVICE_GET_TASKS_TYPE_NOT_INITIALIZE = "Intraservice GetTasks command type was not initialized";

        public const string INTRASERVICE_GET_TASKSLIFETIMES_TYPE_NOT_INITIALIZE = "Intraservice GetTasksLifetimes command type was not initialized";

        public const string EMPTY_STARTOFAPPROVAL_FIELD = "Empty StartOfApproval value";

        public const string ERROR_WHEN_ASSIGNING_TASKLIFETIME_TO_CONTRACT = "Error occured when trying to assign contract {0} to tasklifetime {1}. Details: {2}";

        public const string CRM_CURRENCY_NOT_FOUND = "Did not found currency with the code {0}";
        public const string ERROR_WHEN_ASSIGNING_CURRENCY = "Error occured when tried ot assign currency {0}. Details: {1}";
        public const string ERROR_WHEN_ASSIGNING_CONTRAGENT = "Error occured when tried ot assign contragent(inn:{0};name:{1}) Details: {2}";
        public const string CONTRAGENT_NOT_FOUND = "Contragent with inn {0} not found";

        public const string ERROR_WHILE_SYNCING_TASKLIFETIME = "Error when syncing tasklifetime. Details: {0}";
        public const string ERROR_WHILE_SYNCING_CONTRACT = "Error when syncing contract with id{0}. Details: {1}";

        public const string ERROR_EXTRACTING_NOTIFICATION = "Error when ExtractingNotification. LtId: {0}. Message: {1}";
        public const string ERROR_SENDING_NOTIFICATION = "Error when sending notification fo user {0}. Details: {1}";
    }
}
