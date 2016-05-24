using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    interface IRepository<T> where T:class
    {
        IEnumerable<T> List();
        T Get(int ?id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
