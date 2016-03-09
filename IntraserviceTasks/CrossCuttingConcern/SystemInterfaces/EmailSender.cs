using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.OverdueNotif;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public interface EmailSender
    {
        void Send(Email email);
    }
}
