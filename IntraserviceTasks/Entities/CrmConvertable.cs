﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities
{
    public interface CrmConvertable
    {
        Guid CrmGuid { get; set; }
    }
}
