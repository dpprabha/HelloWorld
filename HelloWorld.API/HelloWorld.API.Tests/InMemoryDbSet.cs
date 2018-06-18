using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.API.Tests
{
    public class InMemoryDbSet<T> : IDbSet<T> where T : class
    {
        readonly HashSet<T> _set;
        readonly HashSet<T> _removed;
        readonly IQueryable<T> _queryableSet;
        private ObservableCollection<T> _localObservableCollection;

        public InMemoryDbSet() : this(Enumerable.Empty<T>()) { }

        public InMemoryDbSet(IEnumerable<T> entities)
        {
            _set = new HashSet<T>();
            _removed = new HashSet<T>();
            _localObservableCollection = new ObservableCollection<T>(Enumerable.Empty<T>());

            foreach (var entity in entities)
            {
                _set.Add(entity);
            }

            _queryableSet = _set.AsQueryable();
        }

        public T Add(T entity)
        {
            _set.Add(entity);
            _localObservableCollection.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            _set.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            var pKeyColumns = GetPrimaryKeyColumns().ToList();
            foreach (var entity in _set)
            {
                var flag = true;
                var pkValues = pKeyColumns.Select(x => x.GetValue(entity)).ToList();
                if (pkValues.Count() != keyValues.Count())
                {
                    flag = false;
                    break;
                }
                for (int i = 0; i < pkValues.Count(); i++)
                {
                    if (!keyValues[i].Equals(pkValues[i]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) return entity;
            }
            return null;
        }

        private IEnumerable<PropertyInfo> GetPrimaryKeyColumns()
        {
            return
                typeof(T).GetProperties()
                    .Where(
                        x =>
                            x.GetCustomAttributes(true)
                                .Any(att => att is System.ComponentModel.DataAnnotations.KeyAttribute));
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get
            {
                if (this._localObservableCollection == null)
                {
                    this._localObservableCollection = new ObservableCollection<T>(this._set ?? Enumerable.Empty<T>());
                }
                return this._localObservableCollection;
            }
        }

        public T Remove(T entity)
        {
            _localObservableCollection.Remove(entity);
            _set.Remove(entity);
            _removed.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return _queryableSet.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _queryableSet.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _queryableSet.Provider; }
        }

        public IEnumerable<T> RemovedEntries
        {
            get { return _removed; }
        }
    }
}