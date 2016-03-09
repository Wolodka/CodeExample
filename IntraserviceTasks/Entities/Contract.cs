using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.Entities
{
    public class Contract : CrmConvertable
    {
        public int Id { get; set; }

        private ContractType _type;
        public ContractType Type 
        {
            get
            {
                if (_type == ContractType.OutgoSpecForRamochnyi)
                    return ContractType.Ramochnyi;
                else
                    return _type;
            }
            set
            {
                _type = value;
            }
        }

        private string _number = "";
        public string Number 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_number))
                    return _number;
                else
                    return Name;
            }
            set
            {
                _number = value;
            }
        }
        
        public string Name { get; set; }
        public int OpportunityNumber { get; set; }
        public bool MustBeChecked { get; set; }
        public List<MustCheckContractReason> CheckReasons { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public DateTime ContractDate { get; set; }
        public string ZkNumber { get; set; }
        public Contragent Contragent { get; set; }

        public Contract()
        {
            Contragent = new Contragent();
            CheckReasons = new List<MustCheckContractReason>();
        }

        public string GetCheckReasons()
        {
            List<string> reasonsStrings = new List<string>();
            foreach(var reason in CheckReasons)
                if( Constants.MUST_CHECK_CONTRACT_REASONS.Keys.Contains(reason) )
                    reasonsStrings.Add(Constants.MUST_CHECK_CONTRACT_REASONS[reason]);

            return string.Join("; ", reasonsStrings.ToArray());
        }


        public Guid CrmGuid { get; set; }
    }
}
