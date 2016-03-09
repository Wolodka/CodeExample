using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmProductionCalendarImpl : ProductionCalendar
    {
        private List<DateTime> _restDays = new List<DateTime>();

        public CrmProductionCalendarImpl()
        {
            Init();
        }

        private void Init()
        {
            var days = GetBusinessClosures().Entities;
            foreach (var singleDay in days)
            {
                DateTime restDay =
                    new DateConverter(singleDay, CalendarAttributes.STARTTIME).GetOrDefault();

                if (restDay != DateTime.MinValue)
                    _restDays.Add(restDay);
            }
        }

        public bool IsRestDay(DateTime day)
        {
            return _restDays.Contains(day.Date);
        }
        
        /// <summary>
        /// Возвращает календарь СРМ
        /// </summary>
        private EntityCollection GetBusinessClosures()
        {
            var query = new QueryExpression(CalendarAttributes.ENTITY_NAME) { ColumnSet = new ColumnSet(true) };
            query.Criteria.AddCondition(CalendarAttributes.NAME,
                                        ConditionOperator.Equal, 
                                        CalendarAttributes.NAME_FOR_INIT_PROD_INSTANCE);

            var closures = CrmService.GetProxy().RetrieveMultiple(query).Entities;
            foreach (var closure in closures)
            {
                return closure.GetAttributeValue<EntityCollection>(CalendarAttributes.CALENDAR_RULES);
            }
            return new EntityCollection();
        }

    }
}
