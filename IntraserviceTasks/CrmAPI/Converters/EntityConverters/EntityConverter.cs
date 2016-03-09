using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters
{
    abstract class EntityConverter<T> where T:CrmConvertable, new()
    {
        protected List<ConvertableAttribute> _attributes;
        protected Entity _crmEntity;
        protected string _crmEntityName;
        protected T _appEntity;

        protected abstract void FillAttributes();
        protected abstract void FillCrmEntityName();

        public EntityConverter()
        {
            FillCrmEntityName();
        }

        public T ConvertToAppEntity(Entity crmEntity)
        {
            _appEntity = new T();
            _crmEntity = crmEntity;
            FillAttributes();
            
            _appEntity.CrmGuid = _crmEntity.Id;

            foreach (var attr in _attributes)
                attr.AssignToAppEntity();

            return _appEntity;
        }

        public Entity ConvertToCrmEntity(T appEntity)
        {
            _appEntity = appEntity;
            _crmEntity = new Entity(_crmEntityName);
            FillAttributes();

            if (appEntity.CrmGuid != Guid.Empty)
                _crmEntity.Id = _appEntity.CrmGuid;

            foreach (var attr in _attributes)
                attr.AssignToCrmEntity();

            return _crmEntity;
        }
    }
}
