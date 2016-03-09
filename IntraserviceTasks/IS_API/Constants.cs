using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.IS_API
{
    class Constants
    {
        public const string IS_DATABASE_KEY = "is_db_prod";

        public static readonly string SQL_SELECT_GET_TASKS_NAMES = 
@"SELECT Id
        ,Name
FROM Task
where Id in 
(	
	select distinct V.[№ Заявки]
	from vw_TaskLifeTimes as V
)";

        public const string SQL_SELECT_GET_SINGLE_TASK_DATA = 
@"SELECT FieldId
    ,Value
    ,TaskId 
FROM vw_TaskAdditionalFields
where TaskId = {0}                                                            
";

        public const string SQL_SELECT_GET_SINGLE_TASK_NAME = 
@"SELECT Name
FROM Task
WHERE Id = {0}";

        public const int TASK_ATTRIBUTE_ID_TOTALAMOUNT = 1022;
        public const int TASK_ATTRIBUTE_ZK_NUMBER = 1030;
        public const int TASK_ATTRIBUTE_ID_CURRENCY = 1032;
        public static readonly Dictionary<Currency, int> CONTRACT_CURRENCY_CODES =
            new Dictionary<Currency, int>
            {
                { Currency.RUR, 37},
                { Currency.USD, 38},
                { Currency.EUR, 39},
                { Currency.Tugrik, 40},
                { Currency.GBP, 69}
            };
        public const int TASK_ATTRIBUTE_ID_CONTRACTTYPE = 1033;
        public static readonly Dictionary<ContractType, int> CONTRACT_TYPE_CODES =
            new Dictionary<ContractType, int>
            {
                { ContractType.Income, 41 },
                { ContractType.Outgo, 42 },
                { ContractType.Ramochnyi, 43 },
                { ContractType.OutgoSpecForRamochnyi, 71 }
            };

        public const int TASK_ATTRIBUTE_ID_CONTRACT_DATE = 1035;
        public const int TASK_ATTRIBUTE_ID_CONTRACTNUMBER = 1036;
        public const int TASK_ATTRIBUTE_ID_OPP_NUMBER = 1047;
        public const int TASK_ATTRIBUTE_ID_CONTRAGENT_NAME = 1048;
        public const int TASK_ATTRIBUTE_ID_CONTRAGENT_INN = 1049;

        public const int INDEX_OF_LIFETIME_ATTR_TASK_ID = 0;
        public const int INDEX_OF_LIFETIME_ATTR_TASK_STATUS = 5;
        public const int INDEX_OF_LIFETIME_ATTR_START_OF_APPROVAL = 6;
        public const int INDEX_OF_LIFETIME_ATTR_APPROVAL_DATE = 7;
        public const int INDEX_OF_LIFETIME_ATTR_APPROVAL_STATUS = 10;
        public const int INDEX_OF_LIFETIME_ATTR_APPROVAL_PERSON_LOGIN = 13;
        public const int INDEX_OF_LIFETIME_ATTR_APPROVAL_ID = 14;
        public const int INDEX_OF_LIFETIME_ATTR_BUSINESS_UNIT = 15;
        
        public static readonly Dictionary<Entities.Enums.TaskStatus, string> TaskStatuses =
            new Dictionary<Entities.Enums.TaskStatus, string>
            {
                { Entities.Enums.TaskStatus.ExpertApproval, "ЭКСПЕРТНОЕ СОГЛАСОВАНИЕ" },
                { Entities.Enums.TaskStatus.Archive, "В АРХИВЕ" },
                { Entities.Enums.TaskStatus.AddingJuristicNotes, "ВНЕСЕНИЕ ЮР. ПРАВОК" },
                { Entities.Enums.TaskStatus.Canceled, "ОТМЕНЕНО" },
                { Entities.Enums.TaskStatus.RegistrationAndSigning, "ОФОРМЛЕНИЕ И ПОДПИСАНИЕ" },
                { Entities.Enums.TaskStatus.SubmissionForApproval, "ПОДАЧА НА СОГЛАСОВАНИЕ" },
                { Entities.Enums.TaskStatus.Signed, "ПОДПИСАН" },
                { Entities.Enums.TaskStatus.AtInitiator, "У ИНИЦИАТОРА" },
                { Entities.Enums.TaskStatus.FinalApproval, "ФИНАЛЬНОЕ СОГЛАСОВАНИЕ" }
            };

        public static readonly Dictionary<ApprovalStatus, string> ApprovalStatuses =
            new Dictionary<ApprovalStatus, string>
            {
                { ApprovalStatus.WasNotApproving, "НЕ СОГЛАСОВЫВАЛ" },
                { ApprovalStatus.LeftComment, "ОСТАВЛЕН КОММЕНТАРИЙ" },
                { ApprovalStatus.Approved, "СОГЛАСОВАНО" }
            };

        public static readonly Dictionary<BusinessUnit, string> BusinessUnits =
            new Dictionary<BusinessUnit, string>
            {
                { BusinessUnit.AIiIT, "АИИИТ" },
                { BusinessUnit.Atrinity, "АТРИНИТИ" }
            };
    }
}
