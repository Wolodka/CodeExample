using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities
{
    public class Contragent : CrmConvertable
    {
        private string _inn = "";
        public string INN 
        {
            get { return _inn; }
            set { _inn = value.Trim(); }
        }
        private string _name = "";
        public string Name 
        {
            get { return _name; }
            set { _name = value.Trim(); } 
        }

        public Contragent()
        {
            INN = "";
            Name = "";
        }

        public Guid CrmGuid { get; set; }
    }
}
