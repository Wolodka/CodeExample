using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.IS_API
{
    class SqlCommandSelectLifeTimes
    {
        private const string SQL_SELECT_GET_TASKLIFETIMES = @"SELECT [№ Заявки]
                                                                      ,[Название заявки]
                                                                      ,[Согласующий]
                                                                      ,[Подразделение согласующего]
                                                                      ,[Тип согласования]
                                                                      ,[Статус]
                                                                      ,[Начало согл.]
                                                                      ,[Окончание согл.]
                                                                      ,[Время согл.]
                                                                      ,[Типовой договор]
                                                                      ,[Статус согласования]
                                                                      ,[Инициатор]
                                                                      ,[Приоритет заявки]
                                                                      ,[ApprovalLogin]
                                                                      ,[ApprovalId]
                                                                      ,[БЕ]
                                                                    FROM [vw_TaskLifeTimes]";

        private const string SQL_WHERE_TASK_ID_CLAUSE = " WHERE [№ Заявки] = {0}";

        private const string SQL_NOT_APPROVED_CLAUSE = " WHERE [Статус согласования] NOT LIKE N'%Согласовано%'";

        public static string GetCommand(IS_GetLifetimes_Type commandType)
        { 
            switch(commandType)
            {
                case IS_GetLifetimes_Type.GetAll:
                    return GetAllCommand();
                    
                case IS_GetLifetimes_Type.GetForSingleTask:
                    return GetSingleTaskLifetimesCommand();

                case IS_GetLifetimes_Type.GetNotApproved:
                    return GetNotApprovedCommand();

                default:
                    throw new Exception(Exceptions.INTRASERVICE_GET_TASKSLIFETIMES_TYPE_NOT_INITIALIZE);
            }
        }

        private static string GetAllCommand()
        { 
            return SQL_SELECT_GET_TASKLIFETIMES;
        }

        private static string GetSingleTaskLifetimesCommand()
        { 
            return string.Format("{0} {1}", SQL_SELECT_GET_TASKLIFETIMES,
                        string.Format(SQL_WHERE_TASK_ID_CLAUSE, Config.ID_OF_INTRASERVICE_TASK_TO_RETRIEVE));
        }

        private static string GetNotApprovedCommand()
        {
            return string.Format("{0} {1}", SQL_SELECT_GET_TASKLIFETIMES, SQL_NOT_APPROVED_CLAUSE);
        }
    }
}
