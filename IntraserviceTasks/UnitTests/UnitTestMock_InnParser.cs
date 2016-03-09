using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestMock_InnParser
    {
        [TestMethod]
        public void TestParseSingleInnString()
        {
            string innString = "7724302834";

            List<string> arrayOfInns = new InnParser(innString).Parse();

            Assert.AreEqual(1, arrayOfInns.Count);
            Assert.AreEqual(innString, arrayOfInns[0]);
        }

        [TestMethod]
        public void TestParseTwoInnsString()
        {
            string innString = "7723153844/772301001";

            List<string> arrayOfInns = new InnParser(innString).Parse();

            Assert.AreEqual(2, arrayOfInns.Count);
            Assert.AreEqual("7723153844", arrayOfInns[0]);
            Assert.AreEqual("772301001", arrayOfInns[1]);
        }

        [TestMethod]
        public void TestParseTwoInnsStringWithWordsINNandKPP()
        {
            string innString = " ИНН 7724093531 / КПП 772401001 ";
            List<string> arrayOfInns = new InnParser(innString).Parse();

            Assert.AreEqual(2, arrayOfInns.Count);
            Assert.AreEqual("7724093531", arrayOfInns[0]);
            Assert.AreEqual("772401001", arrayOfInns[1]);
        }

        [TestMethod]
        public void TestParseWrongInnString()
        {
            TestWrongInnString("Договор №АСТ-2015/09/5408");
            TestWrongInnString("149/6943-23");
            TestWrongInnString("нет");
            
        }

        private void TestWrongInnString(string wrongInn)
        {
            var arrayOfInns = new InnParser(wrongInn).Parse();
            Assert.AreEqual(0, arrayOfInns.Count);
        }
    }
}
