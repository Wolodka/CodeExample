using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.Cheats
{
    [TestClass]
    public class UnitTestReal_CreateContragent
    {
        [TestMethod]
        public void CreateContragent()
        {
            Contragent contragent = new Contragent { INN = "771901001", Name = "ооо \"Энерджи Мастер Компани\"" };

            //new CrmAPI.Cheating.ContragentCreator().Create(contragent); 
        }
    }
}
