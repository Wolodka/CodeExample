using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.Res
{
    class Constants
    {
        public const string LIFETIME_VERSION_FORMAT = "yyyyMMddhhmmss";

        public const int OVERDUE_HOURS_FOR_CASUAL_LIFETIME = 24;
        public const int OVERDUE_HOURS_FOR_IMPORTANT_LIFETIME = 8;
        public const int OVERDUE_HOURS_FOR_STANDART_CONTRACT_LIFETIME = 8;

        public const int WORK_TIME_START_OF_DAY = 9;
        public const int WORK_TIME_FINISH_OF_DAY = 18;

        public const string CONTRACT_NUMBER_PREVIOUS_SIGN = "№";

        public const string CRM_PRODUCTION_KEY = "crmservice_prod";
        public const string CRM_STAGE_KEY = "crmservice_stage";

        public const string INTRASERVICE_TASK_URL_TEMPLATE = @"https://is.asteros.ru/Task/View/{0}";

        public static readonly Dictionary<MustCheckContractReason, string> MUST_CHECK_CONTRACT_REASONS =
                new Dictionary<MustCheckContractReason, string> 
                {
                    { MustCheckContractReason.ContragentIsNotUnique, "few contragents"},
                    { MustCheckContractReason.ContragentNotFound, "contragent not found"},
                    { MustCheckContractReason.WrongContractType, "not equal contract types"},
                    { MustCheckContractReason.WrongOpportunityNumber, "not equal opportunities"},
                    { MustCheckContractReason.FoundMoreThanOneOpportunity, "few opportunities"},
                    { MustCheckContractReason.FewContractsWithSameNumber, "few contracts with same Number"}
                };

        public const string MUST_BE_CHECKED_REASONS_DELIMITER = "; ";

        public const string SLASH_SYMBOL = "/";
        public const string INN_WORD = "ИНН";
        public const string KPP_WORD = "КПП";
    }
}
