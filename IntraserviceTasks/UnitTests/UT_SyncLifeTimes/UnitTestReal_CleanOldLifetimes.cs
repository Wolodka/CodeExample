using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Repositories.CRUD_Lifetimes;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.UT_SyncLifeTimes
{
    [TestClass]
    public class UnitTestReal_CleanOldLifetimes
    {
        [TestMethod]
        public void RemoveOldLifetimes()
        {
            Configuration.Config.UpdateVersion = "20160119094845";
            ExternalSystems.CrmTaskLifeTimesCleaner.Init(new OldVersionsCleaner());
            ExternalSystems.CrmTaskLifeTimesCleaner.Get().CleanOldLifetimes();
        }
    }
}
