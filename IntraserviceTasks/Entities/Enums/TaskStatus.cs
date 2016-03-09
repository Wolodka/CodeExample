using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities.Enums
{
    public enum TaskStatus : int
    {
        Undefined = -1, 
        Archive,
        AddingJuristicNotes,
        Canceled,
        RegistrationAndSigning,
        SubmissionForApproval,
        Signed,
        AtInitiator,
        FinalApproval,
        ExpertApproval
    }
}
