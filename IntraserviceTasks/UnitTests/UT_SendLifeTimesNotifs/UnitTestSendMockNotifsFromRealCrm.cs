using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.OverdueNotif;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.CrmMail;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.UnitTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.UT_SendEmail
{
    [TestClass]
    public class UnitTestSendMockNotifsFromRealCrm
    {
        #region Init
        [TestInitialize()]
        public void Init()
        {
            Config.CrmType = CrmEnvironmentType.Stage;
            InitLifeTimeRetriever();
            InitContractRetriever();
            InitProductionCalendar();
            InitUserRepository();
            InitCrmSystemRepository();
            InitEmailSender();
        }

        private void InitLifeTimeRetriever()
        { 
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>()
            {
                new TaskLifeTime()
                {
                    Id = 100,
                    TaskId = 5555,
                    ApprovalPerson = new Person { Login = MockConstants.KURAEV_USER_LOGIN },
                    StartOfApproval = new DateTime(2015, 12, 1, 10, 15, 30),
                    ApprovalDeadline = new DateTime(2015, 12, 5, 12, 0, 0),
                    TaskPrioriry = Entities.Enums.TaskPriority.High,
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval
                },
                new TaskLifeTime()
                {
                    Id = 101,
                    TaskId = 5555,
                    ApprovalPerson = new Person { Login = MockConstants.KURAEV_USER_LOGIN },
                    StartOfApproval = new DateTime(2015, 12, 1, 10, 15, 30),
                    ApprovalDeadline = new DateTime(2015, 12, 16, 9, 0, 0),
                    TaskPrioriry = Entities.Enums.TaskPriority.High,
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval
                },
                new TaskLifeTime()
                {
                    Id = 102,
                    TaskId = 5553,
                    ApprovalPerson = new Person { Login = MockConstants.KURAEV_USER_LOGIN },
                    StartOfApproval = new DateTime(2015, 12, 3, 10, 15, 30),
                    ApprovalDeadline = new DateTime(2015, 12, 13, 9, 0, 0),
                    TaskPrioriry = Entities.Enums.TaskPriority.High,
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval
                }
            };

            ExternalSystems.CrmTaskLifeTimesRetriever.Init(new Mock.MockTaskLifeTimesRetriever(lifetimes));
        }

        private void InitContractRetriever()
        {
            List<Contract> contracts = new List<Contract>()
            {
                new Contract 
                {
                    Id = 5555,
                    Number = "Test Contract",
                    Name = "Dogovor TestContract",
                    Type = Entities.Enums.ContractType.Income
                },
                new Contract 
                {
                    Id = 5553,
                    Number = "Test Contract 2",
                    Name = "Dogovor TestContract 2",
                    Type = Entities.Enums.ContractType.Outgo
                }
            };

            ExternalSystems.ISContractRetriever.Init(new Mock.MockISContractRetriever(contracts));
        }

        private void InitProductionCalendar()
        { 
            var calendar = new Mock.MockProductionCalendar(new List<DateTime>(), new List<DateTime>());
            ExternalSystems.ProductionCalendar.Init(calendar);
        }

        private void InitUserRepository()
        {
            List<Person> users = new List<Person>
            {
                new Person 
                {
                    CrmGuid = MockConstants.KURAEV_USER_CRM_GUID,
                    Login = MockConstants.KURAEV_USER_LOGIN
                }
            };

            //ExternalSystems.CrmUserRepository.Init(new Mock.MockCrmUserRepository(users));

            ExternalSystems.CrmUserRepository.Init(new CrmUserRepositoryImpl());
        }

        private void InitEmailSender()
        {
            ExternalSystems.EmailSender.Init(new CrmMailSender());
        }

        private void InitCrmSystemRepository()
        {
            ExternalSystems.CrmSystem.Init(new CrmSystemRepository());
        }
        #endregion

        [TestMethod]
        public void TestSendMockNotifications()
        {
            new OverdueNotifier().Notify();
        }
    }
}
