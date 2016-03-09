using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.Res;
using Microsoft.Xrm.Sdk.Client;

namespace IntraserviceTasks.CrmAPI
{
    class CrmService
    {
        private static OrganizationServiceProxy _crmServiceProxy = null;
        private static CrmEnvironmentType _environmentType = CrmEnvironmentType.Stage;

        public static OrganizationServiceProxy GetProxy()
        {
            return GetProxy(Config.CrmType);
        }

        public static OrganizationServiceProxy GetProxy(CrmEnvironmentType environmentType)
        {
            if (_crmServiceProxy == null
                || _environmentType != environmentType)
            {
                _environmentType = environmentType;
                CreateCrmProxy();
            }

            return _crmServiceProxy;
        }

        private static void CreateCrmProxy()
        {
            ClientCredentials cred = new ClientCredentials();
            cred.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            string actualCrmServiceKey = GetCrmServiceConfigKey();
            Uri crmUri = new Uri(ConfigurationManager.AppSettings[actualCrmServiceKey]);

            _crmServiceProxy = new OrganizationServiceProxy(crmUri, null, cred, null);
        }

        private static string GetCrmServiceConfigKey()
        {
            switch (_environmentType)
            {
                case CrmEnvironmentType.Production:
                    return IntraserviceTasks.Res.Constants.CRM_PRODUCTION_KEY;

                case CrmEnvironmentType.Stage:
                    return IntraserviceTasks.Res.Constants.CRM_STAGE_KEY;

                default:
                    throw new ArgumentNullException();
            }
        }
    }
}
