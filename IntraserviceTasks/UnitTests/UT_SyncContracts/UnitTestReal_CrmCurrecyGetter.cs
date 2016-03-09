using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestReal_CrmCurrecyGetter
    {
        [TestInitialize()]
        public void Init()
        {
            ExternalSystems.CurrencyRepository.Init(new CrmCurrencyRepositoryImpl());
        }

        [TestMethod]
        public void TestRealGetRURCurrencyGuid()
        {
            var rep = ExternalSystems.CurrencyRepository.Get();

            Guid curGuid = rep.GetGuid(Entities.Enums.Currency.RUR);

            Assert.AreNotEqual(Guid.Empty, curGuid);
        }
    }
}
