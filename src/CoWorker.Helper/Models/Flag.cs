using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoWorker.Helper.Models
{
    public struct Flag<T> : IFlag<T> where T :IEnumerator<T>
    {
        public static Flag<T> Create(params T[] objs) => new Flag<T>(objs);

        private IEnumerable<T> array;
        private Flag(IEnumerable<T> array)
        {
            this.array = array;
        }

        private int GetHashCode(IEnumerable<T> ts)
            => ts.Select(x => x.GetHashCode()).Aggregate((x, y) => x ^ y);

        public override int GetHashCode()
            => GetHashCode(array);

        public override bool Equals(object obj)
            => obj.GetHashCode() == GetHashCode();

        public bool Equals(T other)
            => array.Contains(other);

        public bool Equals(IEnumerable<T> other)
            => GetHashCode(other) == GetHashCode();

        public IEnumerator<T> GetEnumerator()
            => array.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => array.GetEnumerator();

        public static implicit operator Flag<T>(T[] array) => new Flag<T>(array);
        public static implicit operator T[](Flag<T> union) => union.array.ToArray();

        public static implicit operator Flag<T>(T t) => new Flag<T>(new T[] { t });
        public static implicit operator T (Flag<T> union) => union.array.FirstOrDefault();

        public static bool operator ==(Flag<T> b, T a) => b.array.Contains(a);
        public static bool operator !=(Flag<T> b, T a) => !(b == a);

        public static Flag<T> operator |(Flag<T> a,Flag<T> b) => new Flag<T>(a.array.Union(b.array));
        public static Flag<T> operator ^(Flag<T> a,Flag<T> b) => new Flag<T>(a.array.Union(b.array).Except(a.array.Intersect(b.array)));
    }
}
