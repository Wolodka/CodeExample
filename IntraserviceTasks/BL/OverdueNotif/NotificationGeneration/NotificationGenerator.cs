using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.BL.WorkingTimeCalculation;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class NotificationGenerator
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private ContractCachedSearch _contractsSearch = new ContractCachedSearch();
        private List<TaskLifeTime> _lifetimes;
        private Notifications _notifications = new Notifications();

        public List<NotificationsByUserAndBusinessUnit> GetUsersNotifications()
        {
            GetLifeTimesToNotificate();
            GenerateNotificationsPerUser();
            FilterInactiveUsers();

            return _notifications.NotificationsByUserAndBusinessUnit;
        }

        private void GetLifeTimesToNotificate()
        {
            _lifetimes = ExternalSystems.CrmTaskLifeTimesRetriever.Get().GetOverdued();
        }

        private void GenerateNotificationsPerUser()
        {
            foreach (var lt in _lifetimes)
                TryToHandleSingleLifetime(lt);
        }

        private void TryToHandleSingleLifetime(TaskLifeTime lt)
        {
            try
            {
                HandleSingleLifetime(lt);
            }
            catch (Exception exc)
            {
                _logger.Error(Res.Exceptions.ERROR_EXTRACTING_NOTIFICATION, lt.Id, exc.Message);
            }
        }

        private void HandleSingleLifetime(TaskLifeTime lt)
        {
            if (!_contractsSearch.Find(lt.TaskId))
                return;
                        
            TaskNotification notification = ExtractNotification(lt); 

            _notifications.AppendNotification(lt.ApprovalPerson.Login, lt.BusinessUnit, notification);
        }

        private TaskNotification ExtractNotification(TaskLifeTime lt)
        {
            Contract operatedContract = _contractsSearch.FoundContract;
            return new NotificationExtractorFromLifeTime(operatedContract, lt).ExtractNotification();
        }

        private void FilterInactiveUsers()
        {
            //todo realize it
        }
    }
}
