using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.IS_API
{
    class StringToEnumParser<T>
    {
        public static T Parse(string stringValue, Dictionary<T, string> valuesDictionary)
        {
            string stringValueUpper = stringValue.ToUpper();

            if (valuesDictionary.Values.Contains(stringValueUpper))
                return valuesDictionary.First(o => o.Value == stringValueUpper).Key;

            return (T)(object)-1;
        }
    }
}
