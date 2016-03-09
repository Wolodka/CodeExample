using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntraserviceTasks.CrmAPI
{
    class InnParser
    {
        string _rawInnString;
        List<string> _innNumbers = new List<string>();

        public InnParser(string rawInnString)
        {
            _rawInnString = rawInnString;
        }

        public List<string> Parse()
        { 
            RemoveAllWhiteSpaces();
            RemoveINNandKPPwords();
            SplitBySlash();

            return _innNumbers;
        }

        private void RemoveAllWhiteSpaces()
        { 
            _rawInnString = _rawInnString.Replace(" ", "");
        }

        private void RemoveINNandKPPwords()
        {
            _rawInnString = _rawInnString
                .Replace(Res.Constants.KPP_WORD, "")
                .Replace(Res.Constants.INN_WORD, "");
        }

        private void SplitBySlash()
        {
            MatchCollection mc = Regex.Matches(_rawInnString, @"^([0-9]+/[0-9]+)$|^([0-9]+)$");
            foreach (Match m in mc)
                ExtractInnFromFormattedString(m.Value);
        }

        private void ExtractInnFromFormattedString(string formattedString)
        {
            MatchCollection mc = Regex.Matches(_rawInnString, "[0-9]+");
            foreach (Match m in mc)
                _innNumbers.Add(m.Value);
        }
    }
}
