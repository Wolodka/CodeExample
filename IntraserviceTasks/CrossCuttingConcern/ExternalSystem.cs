using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public class ExternalSystem<T>
    {
        private T _repository;

        public void Init(T repository)
        {
            _repository = repository;
        }

        public T Get()
        {
            if (_repository == null)
                throw new MissingMemberException(String.Format(
                    Res.Exceptions.EXTERNAL_REPOSITORY_WAS_NOT_INITIALIZE, this.GetType().ToString()));

            return _repository;
        }
    }
}
