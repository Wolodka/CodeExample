using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters
{
    interface ConvertableAttribute
    {
        void AssignToCrmEntity();
        void AssignToAppEntity();
    }
}
