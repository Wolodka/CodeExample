#define __PRODUCTION_VERSION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Res
{
    class ConstantsOverdueNotifier
    {
        public static readonly Dictionary<Entities.Enums.ContractType, string> CONTRACT_TYPE_NAMES =
            new Dictionary<Entities.Enums.ContractType, string>
            {
                {Entities.Enums.ContractType.Income,  "Доходный"},
                {Entities.Enums.ContractType.Outgo,   "Расходный"},
                {Entities.Enums.ContractType.Ramochnyi, "Рамочный"},
                {Entities.Enums.ContractType.OutgoSpecForRamochnyi, "Расходный - спецификация к рамочному"}
            };

        public static readonly Dictionary<Entities.Enums.ContractType, bool> CONTRACT_TYPE_IMPORTANCE =
            new Dictionary<Entities.Enums.ContractType, bool>
            {
                {Entities.Enums.ContractType.Income,  true}
            };

        public const string DATE_FORMAT = "«dd» MMMM yyyyг.";

        public const string DURATION_FORMAT_IN_DAYS = "{0} дн";
        public const string DURATION_FORMAT_IN_HOURS = "{0} ч";
        public const string DURATION_FORMAT= "N0";

        //addressats
#if __PRODUCTION_VERSION
        public static readonly Guid NIKOLAEV_USER_CRM_GUID = new Guid("9B8B3CD9-1112-E111-A30F-005056AF6C78");
        public static readonly Guid BELYY_USER_CRM_GUID = new Guid("29A5F65E-7E12-E111-BBFE-005056AF6C78");
        public static readonly Guid KURAEV_USER_CRM_GUID = new Guid("437D389B-4118-E111-B601-005056AF6C78");

        //АИиИТ CC адресаты
        public static readonly Guid LATYSHEV_USER_CRM_GUID = new Guid("F475FC63-3916-E111-869F-005056AF6C78");
        public static readonly Guid KONONENKO_USER_CRM_GUID = new Guid("F36341C3-DDFB-E111-86ED-005056AF6C78");
        public static readonly Guid DAVTYAN_USER_CRM_GUID = new Guid("392760EC-D214-E411-A4C3-005056AF6C78");
        public static readonly Guid SAENKO_USER_CRM_GUID = new Guid("0288BF41-AAA0-E111-B668-005056AF6C78");
        public static readonly Guid FARAKHOV_USER_CRM_GUID = new Guid("ED336C4C-3316-E111-869F-005056AF6C78");
        
        //АТринити CC адресаты
        public static readonly Guid CHUKOV_USER_CRM_GUID = new Guid("5AB1157D-AE37-E311-BBAF-005056AF6C78");
        public static readonly Guid POILOVA_USER_CRM_GUID = new Guid("58DD9E68-9C20-E511-914F-005056931044");
#else
        public static readonly Guid NIKOLAEV_USER_CRM_GUID = new Guid("81129467-6841-E311-8F96-001E0BD53DB4");
        public static readonly Guid BELYY_USER_CRM_GUID = new Guid("D83815E2-2C2C-E311-B8FF-001E0BD53DB4");
        public static readonly Guid KURAEV_USER_CRM_GUID = new Guid("C6444459-3641-E311-8F96-001E0BD53DB4");

        //АИиИТ CC адресаты
        public static readonly Guid LATYSHEV_USER_CRM_GUID = new Guid("5232649F-A98D-E311-BB35-001E0BD53DB4");
        public static readonly Guid KONONENKO_USER_CRM_GUID = new Guid("598EC474-A98D-E311-BB35-001E0BD53DB4");
        public static readonly Guid DAVTYAN_USER_CRM_GUID = new Guid("B504CE08-A98D-E311-BB35-001E0BD53DB4");
        public static readonly Guid SAENKO_USER_CRM_GUID = new Guid("6B868F12-AA8D-E311-BB35-001E0BD53DB4");
        public static readonly Guid FARAKHOV_USER_CRM_GUID = new Guid("22B33167-AA8D-E311-BB35-001E0BD53DB4");
        
        //АТринити CC адресаты
        public static readonly Guid CHUKOV_USER_CRM_GUID = new Guid("40A08D85-AA8D-E311-BB35-001E0BD53DB4");
        public static readonly Guid POILOVA_USER_CRM_GUID = new Guid("5A841DFA-A98D-E311-BB35-001E0BD53DB4");
#endif

        public static readonly List<Guid> AIiIT_CC_ADDRESSATS_GUIDS = new List<Guid> 
            { 
                LATYSHEV_USER_CRM_GUID,
                KONONENKO_USER_CRM_GUID,
                DAVTYAN_USER_CRM_GUID,
                NIKOLAEV_USER_CRM_GUID,
                SAENKO_USER_CRM_GUID,
                FARAKHOV_USER_CRM_GUID
            };

        public static readonly List<Guid> ATRINITY_CC_ADDRESSATS_GUIDS = new List<Guid> 
            { 
                CHUKOV_USER_CRM_GUID,
                POILOVA_USER_CRM_GUID,
                NIKOLAEV_USER_CRM_GUID
            };

    }
}
