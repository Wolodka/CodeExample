using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.IS_API
{
    class ContractAttributeAppender
    {
        private Contract _contract;
        private SqlDataReader _dbRow;
        private KeyValuePair<int, string> _attribute;

        public ContractAttributeAppender(Contract contract, SqlDataReader row)
        {
            _contract = contract;
            _dbRow = row;
        }

        public void Append()
        {
            ExtractAttributeFromDBRow();
            AppendContractAttribute();
        }

        private void ExtractAttributeFromDBRow()
        {
            int id = _dbRow.GetInt32(0);
            string value = _dbRow.GetString(1);

            _attribute = new KeyValuePair<int, string>(id, value);
        }

        private void AppendContractAttribute()
        {
            switch (_attribute.Key)
            {
                case Constants.TASK_ATTRIBUTE_ID_TOTALAMOUNT:
                    decimal netvalue;
                    if (decimal.TryParse(_attribute.Value.Replace('.', ','), out netvalue))
                        _contract.TotalAmount = netvalue;
                    break;

                case Constants.TASK_ATTRIBUTE_ZK_NUMBER:
                    _contract.ZkNumber = _attribute.Value;
                    break;

                case Constants.TASK_ATTRIBUTE_ID_CURRENCY:
                    int currencyCode;
                    if (int.TryParse(_attribute.Value, out currencyCode))
                    {
                        if (Constants.CONTRACT_CURRENCY_CODES.Values.Contains(currencyCode))
                            _contract.Currency = Constants.CONTRACT_CURRENCY_CODES.First(o => o.Value == currencyCode).Key;
                    }

                    break;

                case Constants.TASK_ATTRIBUTE_ID_CONTRACTTYPE:
                    int docTypeInt;
                    if (int.TryParse(_attribute.Value, out docTypeInt))
                    {
                        if (Constants.CONTRACT_TYPE_CODES.Values.Contains(docTypeInt))
                            _contract.Type = Constants.CONTRACT_TYPE_CODES.First(o => o.Value == docTypeInt).Key;
                    }
                    break;

                case Constants.TASK_ATTRIBUTE_ID_CONTRACT_DATE:
                    try
                    {
                        _contract.ContractDate = DateTime.ParseExact(_attribute.Value,
                            "yyyy-MM-dd HH:mm",
                            System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    { }
                    break;

                case Constants.TASK_ATTRIBUTE_ID_CONTRACTNUMBER:
                    _contract.Number = _attribute.Value;
                    break;

                case Constants.TASK_ATTRIBUTE_ID_OPP_NUMBER:
                    int crmOpportunityNumber;
                    if (int.TryParse(_attribute.Value, out crmOpportunityNumber))
                        _contract.OpportunityNumber = crmOpportunityNumber;
                    break;

                case Constants.TASK_ATTRIBUTE_ID_CONTRAGENT_NAME:
                    _contract.Contragent.Name = _attribute.Value;
                    break;

                case Constants.TASK_ATTRIBUTE_ID_CONTRAGENT_INN:
                    _contract.Contragent.INN = _attribute.Value;
                    break;

                default:
                    break;
            }
        }

    }
}
