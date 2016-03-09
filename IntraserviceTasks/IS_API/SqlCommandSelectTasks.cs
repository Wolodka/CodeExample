using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.IS_API
{
    class SqlCommandSelectTasks
    {
        private const string GET_TASKS_DATA = 
            @"SELECT    FieldId
                        ,Value
                        ,TaskId 
            FROM vw_TaskAdditionalFields
            ";

        private const string WHERE_TASKIDS_IN =
            " where TaskId in ({0}) ";

        private const string SQL_SELECT_TOP1000TASKS_TEMPLATE =
            @"  select top 1000 InnerReq.NumZav
                from ( {0} ) as InnerReq
	            order by InnerReq.NumZav desc
            ";
        private const string SQL_SELECT_TASKS_IDS_FROM_TASKLIFETIMESVIEW =
            @"  select distinct V.[№ Заявки] as NumZav
	            from vw_TaskLifeTimes as V";

        private const string ORDER = " ORDER BY TaskId desc ";

        private const int TEST_INTRASERVICE_TASK_ID = -1;


        public static string GetCommand(IS_GetTasks_Type commandType)
        {
            switch (commandType)
            { 
                case IS_GetTasks_Type.GetSingleTask:
                    return GetCommand_GetSingleTaskData();

                case IS_GetTasks_Type.GetTop1000Tasks:
                    return GetCommand_GetTop1000TasksData();

                case IS_GetTasks_Type.GetAllTasks_fromTaskLifeTimesView:
                    return GetCommand_GetAllTasksData();

                default:
                    throw new Exception(Exceptions.INTRASERVICE_GET_TASKS_TYPE_NOT_INITIALIZE);
            }
        }

        private static string GetCommand_GetAllTasksData()
        {
            return String.Format("{0} {1} {2}",
                GET_TASKS_DATA,
                String.Format(WHERE_TASKIDS_IN, SQL_SELECT_TASKS_IDS_FROM_TASKLIFETIMESVIEW),
                ORDER);

        }

        private static string GetCommand_GetTop1000TasksData()
        {
            return String.Format("{0} {1} {2}",
                        GET_TASKS_DATA,
                        String.Format(WHERE_TASKIDS_IN,
                            String.Format(SQL_SELECT_TOP1000TASKS_TEMPLATE, SQL_SELECT_TASKS_IDS_FROM_TASKLIFETIMESVIEW)),
                        ORDER);
        }

        private static string GetCommand_GetSingleTaskData()
        {


            return String.Format("{0} {1}",
                        GET_TASKS_DATA,
                        String.Format(WHERE_TASKIDS_IN, Config.ID_OF_INTRASERVICE_TASK_TO_RETRIEVE));
        }


    }
}
