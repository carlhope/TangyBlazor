using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    internal interface IDataAccess<T>
    {
        public Task<T> Create(T objDTO);
        public Task<T> Update(T objDTO);
        public Task<int> Delete(int id);

        public Task<T> Get(int id);

        public Task<IEnumerable<T>> GetAll(int? id = null);
    }
}
