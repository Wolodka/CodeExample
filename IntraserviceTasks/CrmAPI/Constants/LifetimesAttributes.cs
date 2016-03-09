using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.CrmAPI.Constants
{
    class LifetimesAttributes
    {
        public const string ENTITY_NAME = "ac_contractagreement";
        public const string ID = "ac_contractagreementid";

        public const string INTRASERVICE_TASK_ID = "ac_intraservice_task_id";
        public const string INTRASERVICE_ID = "ac_intraservice_id";
        public const string APPROVAL_DATE = "ac_agreementdate";
        public const string START_OF_APPROVAL = "ac_sendtodate";
        public const string APPROVAL_DEADLINE = "ac_agreement_deadline";
        public const string APPROVAL_STATUS = "statuscode";

        public const string APPROVAL_PERSON = "ac_agreeduserid";
        public const string AGREED_PERSON = "ac_agreeduserid";
        public const string CONTRACT = "ac_contractid";
        public const string BUSINESS_UNIT = "ac_businessunit";

        public const string VERSION = "ac_version";
    }
}
