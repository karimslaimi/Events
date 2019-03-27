using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IservicePattern<T>:IDisposable where T:class
    {

        //this interface is used for CRUD (Create,Retrieve,Update,Delete) operations 
        //it s generic so it works on any type of entity
        //it s alread

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
       // IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> orderBy = null);
        T Get(Expression<Func<T, bool>> where);

        void Commit();
    }
}
