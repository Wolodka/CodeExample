using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockCrmContragents : CrmContragentRepository
    {
        List<Contragent> _contragets;

        public MockCrmContragents(List<Contragent> contragents)
        {
            _contragets = contragents;
        }

        public List<Contragent> GetByCompositeInnString(string INN)
        {
            string upperINN = INN.Trim().ToUpper();

            return _contragets.Where(o => o.INN.ToUpper() == upperINN).ToList();
        }


        public Contragent Get(Guid crmGuid)
        {
            return _contragets.First(o => o.CrmGuid == crmGuid);
        }
    }
}
