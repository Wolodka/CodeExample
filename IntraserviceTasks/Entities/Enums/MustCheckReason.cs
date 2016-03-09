using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities.Enums
{
    public enum MustCheckContractReason
    {
        ContragentIsNotUnique,
        ContragentNotFound,
        WrongContractType,
        WrongOpportunityNumber,
        FoundMoreThanOneOpportunity,
        FewContractsWithSameNumber
    }
}
