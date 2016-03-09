using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockCrmUserRepository : CrmUserRepository
    {
        private List<Person> _users;

        public MockCrmUserRepository(List<Person> users)
        {
            _users = users;
        }

        public Person GetByGuid(Guid crmGuid)
        {
            return _users.First(o => o.CrmGuid == crmGuid);
        }

        public Guid GetGuidByLogin(string loginWithoutDomain)
        {
            return _users.First(o => o.Login.ToUpper() == loginWithoutDomain.ToUpper()).CrmGuid;
        }
    }
}
