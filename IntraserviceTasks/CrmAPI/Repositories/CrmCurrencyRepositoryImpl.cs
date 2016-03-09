using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmCurrencyRepositoryImpl : CrmCurrencyRepository
    {
        private static Dictionary<string, Guid> _currencyCodesCache = new Dictionary<string, Guid>();

        public Guid GetGuid(Currency currency)
        {
            string code = GetCurrencyCode(currency);

            if (ExistsInCache(code))
                return GetGuidFromCache(code);

            Guid currencyGuid = ExtractCurrencyGuidFromCrm(code);
            SaveCurrencyGuidInCache(code, currencyGuid);

            return currencyGuid;
        }

        private string GetCurrencyCode(Currency currency)
        {
            return CurrencyAttributes.CURRENCY_CODES[currency];
        }

        private bool ExistsInCache(string code)
        {
            return _currencyCodesCache.Keys.Contains(code);
        }

        private Guid GetGuidFromCache(string code)
        {
            return _currencyCodesCache[code];
        }

        private Guid ExtractCurrencyGuidFromCrm(string code)
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = CurrencyAttributes.ENTITY_NAME;
            query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression(CurrencyAttributes.CURRENCY_CODE,
                                                                    ConditionOperator.Like,
                                                                    String.Format("%{0}%", code)));

            var crmService = CrmService.GetProxy();
            var contracts = crmService.RetrieveMultiple(query);

            if (contracts.Entities.Count > 0)
                return contracts[0].Id;

            throw new KeyNotFoundException(String.Format(Exceptions.CRM_CURRENCY_NOT_FOUND, code));
        }

        private void SaveCurrencyGuidInCache(string code, Guid id)
        {
            _currencyCodesCache[code] = id;
        }

        public Currency GetCurrency(Guid currencyGuid)
        {
            string code;

            if (ExistsInCache(currencyGuid))
            {
                code = GetCodeFromCache(currencyGuid);
            }
            else
            {
                code = ExtractCurrencyCodeFromCrm(currencyGuid);
                SaveCurrencyGuidInCache(code, currencyGuid);
            }

            code = code.ToUpper();
            return CurrencyAttributes.CURRENCY_CODES.First(o => o.Value.ToUpper() == code).Key;
        }

        private bool ExistsInCache(Guid currencyGuid)
        {
            return _currencyCodesCache.Values.Contains(currencyGuid);
        }

        private string GetCodeFromCache(Guid currencyGuid)
        {
            return _currencyCodesCache.First(o => o.Value == currencyGuid).Key;
        }

        private string ExtractCurrencyCodeFromCrm(Guid currencyGuid)
        {
            var crmService = CrmService.GetProxy();
            var crmCurrency = crmService.Retrieve(CurrencyAttributes.ENTITY_NAME, currencyGuid, new ColumnSet(true));
            return new StringConverter(crmCurrency, CurrencyAttributes.CURRENCY_CODE).GetOrDefault();
        }
    }
}
