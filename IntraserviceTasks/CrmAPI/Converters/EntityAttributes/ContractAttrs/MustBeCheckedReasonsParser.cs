using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrmAPI.Converters.ContractAttrsAttrs
{
    class MustBeCheckedReasonsParser
    {
        private List<MustCheckContractReason> _reasons = new List<MustCheckContractReason>();
        private string _crmReasonsString = "";
        private string[] _reasonsStringArray;

        public MustBeCheckedReasonsParser(string crmReasonsString)
        {
            _crmReasonsString = crmReasonsString;
        }

        public List<MustCheckContractReason> Parse()
        {
            if (String.IsNullOrWhiteSpace(_crmReasonsString))
                return new List<MustCheckContractReason>();

            GetReasonsStringArray();

            foreach (var reasonString in _reasonsStringArray)
                AppendSingleReason(reasonString);

            return _reasons;
        }

        private void GetReasonsStringArray()
        {
            if (_crmReasonsString.IndexOf(IntraserviceTasks.Res.Constants.MUST_BE_CHECKED_REASONS_DELIMITER) > 0)
            {
                _reasonsStringArray = _crmReasonsString.Split(
                    new string[] { IntraserviceTasks.Res.Constants.MUST_BE_CHECKED_REASONS_DELIMITER },
                    StringSplitOptions.None);
            }
            else
                _reasonsStringArray = new string[] { _crmReasonsString };
        }

        private void AppendSingleReason(string reasonString)
        {
            if (IntraserviceTasks.Res.Constants.MUST_CHECK_CONTRACT_REASONS.Values.Contains(reasonString))
                _reasons.Add(IntraserviceTasks.Res.Constants.MUST_CHECK_CONTRACT_REASONS.First(o => o.Value == reasonString).Key);
        }
    }
}
