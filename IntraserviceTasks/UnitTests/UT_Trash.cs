using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.IS_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UT_Trash
    {
        [TestMethod]
        public void Test_GetUpdateVersion()
        {
            DateTime now = new DateTime(2015, 12, 29, 11, 21, 33);

            string version = now.ToString(Res.Constants.LIFETIME_VERSION_FORMAT);

            Assert.AreEqual("20151229112133", version);
        }
    }
}
