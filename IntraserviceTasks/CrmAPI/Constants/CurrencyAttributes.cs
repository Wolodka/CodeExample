using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrmAPI.Constants
{
    class CurrencyAttributes
    {
        public const string ENTITY_NAME = "transactioncurrency";
        public const string ID = "transactioncurrencyid";
        public const string CURRENCY_CODE = "isocurrencycode";

        public static readonly Dictionary<Currency, string> CURRENCY_CODES =
            new Dictionary<Currency, string> 
            { 
                { Currency.RUR, "RUB" },
                { Currency.USD, "USD" },
                { Currency.EUR, "EUR" },
                { Currency.Tugrik, "MNT" },
                { Currency.GBP, "GBP" }
            };
    }
}
