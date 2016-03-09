using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.BL.OverdueNotif;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.CrmMail;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.IS_API;
using NLog;
using TaskApprovalNotifier.Res;

namespace TaskApprovalNotifier
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            _logger.Info(LogMessages.PROGRAMM_STARTED);

            var intMax = Int32.MaxValue;

            //SyncTaskLifetimesWithCrm();
            SendNotifications();

            _logger.Info(LogMessages.PROGRAMM_FINISHED);
            Console.ReadLine();
        }

        class StartConditions
        {
            public static void Init()
            {
                InitConfigParams();
                InitExternalSystems();
            }
            
            private static void InitConfigParams()
            {
                Config.CrmType = CrmEnvironmentType.Stage;
                Config.Crm_GetContracts_CommandType = Crm_GetContracts_Type.GetAll;
                Config.IS_GetTasks_CommandType = IS_GetTasks_Type.GetAllTasks_fromTaskLifeTimesView;
                Config.IS_GetLifetimes_CommandType = IS_GetLifetimes_Type.GetNotApproved;
                Config.ID_OF_INTRASERVICE_TASK_TO_RETRIEVE = 1003;
            }

            private static void InitExternalSystems()
            {
                ExternalSystems.ISContractRetriever.Init(new ISContractRetriever());
                ExternalSystems.ISTaskLifeTimesRetriever.Init(new ISTaskLifetimeRetriever());
                ExternalSystems.CrmContractsRepository.Init(new CrmContractsRepositoryImpl());
                ExternalSystems.CrmTaskLifeTimesRetriever.Init(new CrmLifetimesRepositoryImpl());
                ExternalSystems.CrmTaskLifeTimesRepository.Init(new CrmLifetimesRepositoryImpl());
                ExternalSystems.CrmTaskLifeTimesCleaner.Init
                    (new IntraserviceTasks.CrmAPI.Repositories.CRUD_Lifetimes.OldVersionsCleaner());
                ExternalSystems.CrmUserRepository.Init(new CrmUserRepositoryImpl());
                ExternalSystems.CrmSystem.Init(new CrmSystemRepository());
                ExternalSystems.CurrencyRepository.Init(new CrmCurrencyRepositoryImpl());
                ExternalSystems.ProductionCalendar.Init(new CrmProductionCalendarImpl());
                ExternalSystems.ContragentRepository.Init(new CrmContragentRepositoryImpl());
                ExternalSystems.EmailSender.Init(new CrmMailSender());
            }
        }
        
        private static void SyncContractsWithCrm()
        {
            _logger.Info(LogMessages.SYNC_CONTRACTS_STARTED);

            StartConditions.Init();
            new ContractSynchronizator().Sync();
        }

        private static void SyncTaskLifetimesWithCrm()
        {
            _logger.Info(LogMessages.SYNC_APPROVALS_STARTED);

            StartConditions.Init();
            new LifeTimesSynchronizator().Sync();
        }

        private static void SendNotifications()
        {
            StartConditions.Init();
            new OverdueNotifier().Notify();
        }
    }
}
