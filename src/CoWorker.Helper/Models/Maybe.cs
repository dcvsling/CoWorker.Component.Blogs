namespace CoWorker.Helper.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// may have zero ,one ,or more
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Maybe<T> : IEnumerable<T>,IEqualityComparer<T>,IEquatable<T>,IEqualityComparer<Maybe<T>>,IEqualityComparer<IEnumerable<T>>,IDisposable
    {
        public static Maybe<T> New(params T[] ts) => new Maybe<T>(ts);

        public IEnumerator<T> GetEnumerator()
            => buffer.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => buffer.GetEnumerator();

        public bool Equals(T x, T y)
            => x.Equals(y);

        public int GetHashCode(T obj)
            => obj.GetHashCode();

        public bool Equals(T other)
            => GetHashCode() == other.GetHashCode();

        public bool Equals(Maybe<T> x, Maybe<T> y)
            => x.Equals(y);

        public int GetHashCode(Maybe<T> obj)
            => obj.Aggregate(0, (seed, o) => o.GetHashCode() ^ seed);

        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            => x.Except(y).Any();

        public int GetHashCode(IEnumerable<T> obj)
            => obj.Aggregate(0, (seed, o) => o.GetHashCode() ^ seed);

        private IEnumerable<T> buffer;
        private Maybe(params T[] ts)
        {
            buffer = ts.Length == 0 ? Enumerable.Empty<T>() : ts;
        }

        private Maybe(IEnumerable<T> ts) => buffer = ts;

        public Maybe<TOther> Cast<TOther>() => new Maybe<TOther>(buffer.Cast<TOther>());

        private bool isDispose = false;
        

        public static implicit operator T(Maybe<T> maybe) => maybe.buffer.FirstOrDefault();
        public static implicit operator T[](Maybe<T> maybe) => maybe.buffer.ToArray();

        public static implicit operator Maybe<T>(T t) => New(t);
        public static implicit operator Maybe<T> (T[] ts) => New(ts);

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    buffer.Each(x => (x as IDisposable).Dispose());   
                }
                buffer = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
