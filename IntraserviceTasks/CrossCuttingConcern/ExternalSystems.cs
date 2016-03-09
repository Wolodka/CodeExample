using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public class ExternalSystems
    {
        // CRM
        private static ExternalSystem<CrmContractsRepository> _crmContractsRepository 
            = new ExternalSystem<CrmContractsRepository>();
        public static ExternalSystem<CrmContractsRepository> CrmContractsRepository
        {
            get { return _crmContractsRepository; }
        }

        private static ExternalSystem<CrmLifetimeRepository> _crmLifetimeRepository
            = new ExternalSystem<CrmLifetimeRepository>();
        public static ExternalSystem<CrmLifetimeRepository> CrmTaskLifeTimesRepository
        {
            get { return _crmLifetimeRepository; }
        }

        private static ExternalSystem<TaskLifeTimesRetriever> _crmLifetimeRetriever
            = new ExternalSystem<TaskLifeTimesRetriever>();
        public static ExternalSystem<TaskLifeTimesRetriever> CrmTaskLifeTimesRetriever
        {
            get { return _crmLifetimeRetriever; }
        }

        private static ExternalSystem<CrmLifetimesCleaner> _crmLifetimeCleaner
            = new ExternalSystem<CrmLifetimesCleaner>();
        public static ExternalSystem<CrmLifetimesCleaner> CrmTaskLifeTimesCleaner
        {
            get { return _crmLifetimeCleaner; }
        }

        private static ExternalSystem<CrmUserRepository> _crmUserRepository
            = new ExternalSystem<CrmUserRepository>();
        public static ExternalSystem<CrmUserRepository> CrmUserRepository
        {
            get { return _crmUserRepository; }
        }

        private static ExternalSystem<ProductionCalendar> _productionCalendar
            = new ExternalSystem<ProductionCalendar>();
        public static ExternalSystem<ProductionCalendar> ProductionCalendar
        {
            get { return _productionCalendar; }
        }

        private static ExternalSystem<EmailSender> _emailSender
            = new ExternalSystem<EmailSender>();
        public static ExternalSystem<EmailSender> EmailSender
        {
            get { return _emailSender; }
        }

        private static ExternalSystem<CrmCurrencyRepository> _currencyRepository
            = new ExternalSystem<CrmCurrencyRepository>();
        public static ExternalSystem<CrmCurrencyRepository> CurrencyRepository
        {
            get { return _currencyRepository; }
        }

        private static ExternalSystem<CrmSystem> _crmSystem
            = new ExternalSystem<CrmSystem>();
        public static ExternalSystem<CrmSystem> CrmSystem
        {
            get { return _crmSystem; }
        }

        private static ExternalSystem<CrmContragentRepository> _contragentRepository
            = new ExternalSystem<CrmContragentRepository>();
        public static ExternalSystem<CrmContragentRepository> ContragentRepository
        {
            get { return _contragentRepository; }
        }

        // INTRASERVICE
        private static ExternalSystem<TaskLifeTimesRetriever> _isLifetimeRepository
            = new ExternalSystem<TaskLifeTimesRetriever>();
        public static ExternalSystem<TaskLifeTimesRetriever> ISTaskLifeTimesRetriever
        {
            get { return _isLifetimeRepository; }
        }

        private static ExternalSystem<ContractRetriever> _isContractRetriever
            = new ExternalSystem<ContractRetriever>();
        public static ExternalSystem<ContractRetriever> ISContractRetriever
        {
            get { return _isContractRetriever; }
        }
        


        private ExternalSystems()
        { }

    }
}
