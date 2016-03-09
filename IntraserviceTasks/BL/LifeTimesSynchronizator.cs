using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.BL
{
    public class LifeTimesSynchronizator
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private List<TaskLifeTime> _lifetimes;

        public void Sync()
        {
            RetrieveISLifetimes();
            SyncLifetimes();
            RemoveOldLifetimes();
        }

        private void RetrieveISLifetimes()
        {
            TaskLifeTimesRetriever ISRetriever = ExternalSystems.ISTaskLifeTimesRetriever.Get();
            _lifetimes = ISRetriever.Get();
        }

        private void SyncLifetimes()
        {
            foreach (var lifetime in _lifetimes)
                TryToSyncLifeTime(lifetime);
        }

        private void TryToSyncLifeTime(TaskLifeTime lifetime)
        {
            try
            {
                new SingleLifeTimeSynchronizator(lifetime).Sync();
                _logger.Info(LogMessages.FINISHED_SYNC_OF_SINGLE_LIFETIME_WITH_ID, lifetime.Id.ToString());
            }
            catch (Exception exc)
            {
                _logger.Error(Exceptions.ERROR_WHILE_SYNCING_TASKLIFETIME, exc.Message);
            }
        }

        class SingleLifeTimeSynchronizator
        {
            private TaskLifeTime _lifetime;
            private CrmLifetimeRepository _destination;
            
            public SingleLifeTimeSynchronizator(TaskLifeTime lifetime)
            {
                _destination = ExternalSystems.CrmTaskLifeTimesRepository.Get();
                _lifetime = lifetime;
            }

            public void Sync()
            {
                AssignVersion();
                AssignOrClearDeadline();
                SyncSingleLifeTime();
            }

            private void AssignVersion()
            {
                _lifetime.Version = Configuration.Config.UpdateVersion;
            }

            private void AssignOrClearDeadline()
            {
                if (NeedToAssignApprovalDeadline())
                    AssignApprovalDeadline();
                else
                    ClearApprovalDeadlineValue();
            }

            private bool NeedToAssignApprovalDeadline()
            {
                return _lifetime.TaskStatus == Entities.Enums.TaskStatus.ExpertApproval
                        && _lifetime.ApprovalStatus == ApprovalStatus.WasNotApproving;
            }

            private void AssignApprovalDeadline()
            {
                if (_lifetime.StartOfApproval == DateTime.MinValue)
                    throw new MissingFieldException(Exceptions.EMPTY_STARTOFAPPROVAL_FIELD);

                new DeadlineAssigner(_lifetime).Assign();
            }

            private void ClearApprovalDeadlineValue()
            {
                _lifetime.ApprovalDeadline = DateTime.MinValue;
            }

            private void SyncSingleLifeTime()
            {
                if (_destination.IfExists(_lifetime))
                    _destination.Update(_lifetime);
                else
                    _destination.Create(_lifetime);
            }
        }

        private void RemoveOldLifetimes()
        {
            ExternalSystems.CrmTaskLifeTimesCleaner.Get().CleanOldLifetimes();
        }
    }
}
