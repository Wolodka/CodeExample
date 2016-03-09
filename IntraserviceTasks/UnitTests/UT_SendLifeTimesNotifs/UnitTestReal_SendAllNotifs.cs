using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.BL.OverdueNotif;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.CrmMail;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestSendAllNotifs
    {
        [TestInitialize()]
        public void Init()
        {
            Config.CrmType = CrmEnvironmentType.Stage;
            ExternalSystems.ISContractRetriever.Init(new IS_API.ISContractRetriever());
            ExternalSystems.ISTaskLifeTimesRetriever.Init(new IS_API.ISTaskLifetimeRetriever());
            ExternalSystems.CrmContractsRepository.Init(new CrmAPI.Repositories.CrmContractsRepositoryImpl());
            ExternalSystems.CrmTaskLifeTimesRetriever.Init(new CrmLifetimesRepositoryImpl());
            ExternalSystems.CrmTaskLifeTimesRepository.Init(new CrmLifetimesRepositoryImpl());
            ExternalSystems.CrmUserRepository.Init(new CrmUserRepositoryImpl());
            ExternalSystems.ProductionCalendar.Init(new CrmProductionCalendarImpl());
            ExternalSystems.CrmSystem.Init(new CrmSystemRepository());
            ExternalSystems.EmailSender.Init(new CrmMailSender());
        }

        [TestMethod]
        public void TestSendAllNotifications()
        {
            //new OverdueNotifier().Notify();
        }
    }
}
