using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.Converters
{
    abstract class AppEntityConvertableAttribute<AppEntityType, ConverterOperableType> : ConvertableAttribute
    {
        protected AppEntityType _appEntity;
        protected AttributeConverter<ConverterOperableType> _converter;

        public AppEntityConvertableAttribute(AppEntityType appEntity, AttributeConverter<ConverterOperableType> converter)
        {
            _appEntity = appEntity;
            _converter = converter;
        }

        public abstract void AssignToCrmEntity();
        public abstract void AssignToAppEntity();
    }
}
