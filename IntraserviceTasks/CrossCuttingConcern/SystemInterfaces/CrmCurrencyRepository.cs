using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrossCuttingConcern.SystemInterfaces
{
    public interface CrmCurrencyRepository
    {
        Guid GetGuid(Currency currency);
        Currency GetCurrency(Guid currencyGuid);
    }
}
