using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.Helper.Models
{
    public struct AbstractMember<T>
    {
        private static Func<T> DefaultGetter => () => default(T);
        private static Action<T> DefaultSetter
            => x =>
            {
            };
        private Func<T> getter;
        private Action<T> setter;
        public AbstractMember(string Name,Func<T> getter = null,Action<T> setter = null)
        {
            this.getter = getter ?? DefaultGetter;
            this.setter = setter ?? DefaultSetter;
            this.Name = Name;
        }

        public string Name { get; }

        public static implicit operator T(AbstractMember<T> m) => m.getter();
    }
}
