
namespace CoWorker.Helper.Models.Generic
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using CoWorker.Helper.Models;
    
    public class GenericList : IGenericList
    {
        private IDictionary<Type, Maybe<object>> map;

        public GenericList(bool isSync = true)
        {
            map = isSync 
                ? new Dictionary<Type, Maybe<object>>() as IDictionary<Type, Maybe<object>> 
                : new ConcurrentDictionary<Type, Maybe<object>>();
        }

        internal GenericList(IEnumerable<KeyValuePair<Type, Maybe<object>>> array)
        {
            map = array.ToDictionary(x => x.Key, x => x.Value);
        }

        public void Add<T>(object obj)
        {
            var maybe = obj as Maybe<object> ?? Maybe<object>.New();
            maybe = obj.GetType().HasElementType ? (IEnumerable)obj : obj;
            map.Add(new KeyValuePair<Type, Maybe<object>>(obj.GetType(), maybe));
        }

        #region Dictionary implements
        
        public ICollection<Type> Keys => map.Keys;

        public ICollection<Maybe<object>> Values => map.Values;

        public int Count => map.Count;

        public bool IsReadOnly => map.IsReadOnly;

        public Maybe<object> this[Type key] { get => map[key]; set => map[key] = value; }

        public void Add(Type key, Maybe<object> value)
        {
            map.Add(key, value);
        }

        public bool ContainsKey(Type key)
        {
            return map.ContainsKey(key);
        }

        public bool Remove(Type key)
        {
            return map.Remove(key);
        }

        public bool TryGetValue(Type key, out Maybe<object> value)
        {
            return map.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<Type, Maybe<object>>>.Add(KeyValuePair<Type, Maybe<object>> item)
        {
            map.Add(item);
        }

        public void Clear()
        {
            map.Clear();
        }

        public bool Contains(KeyValuePair<Type, Maybe<object>> item)
        {
            return map.Contains(item);
        }

        public void CopyTo(KeyValuePair<Type, Maybe<object>>[] array, int arrayIndex)
        {
            map.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Type, Maybe<object>> item)
        {
            return map.Remove(item);
        }

        public IEnumerator<KeyValuePair<Type, Maybe<object>>> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return map.GetEnumerator();
        }
        


        #endregion
    }
}
