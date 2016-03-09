using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities
{
    public class Person : CrmConvertable
    {
        public string Name { get; set; }
        
        private string _login;
        public string Login 
        {
            get 
            { 
                return _login; 
            }
            set
            {
                _login = value;
                CleanLoginFromDomainName();
            }
        }

        private void CleanLoginFromDomainName()
        {
            _login = _login.ToLower();

            //1) ..@bacint.ru
            if (_login.IndexOf("@") > 0)
                _login = _login.Substring(0, _login.IndexOf("@"));
            //2) bacint\...
            else
            {
                int indexBacint = _login.IndexOf("bacint", StringComparison.CurrentCultureIgnoreCase);
                if (indexBacint >= 0)
                    _login = _login.Substring(indexBacint + 7);
            }

        }
        
        public bool ActiveInCrm { get; set; }
        
        public Guid CrmGuid { get; set; }
    }
}
