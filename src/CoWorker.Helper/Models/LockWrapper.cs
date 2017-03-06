using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremeGamer.MainSite.Server.Models
{
    public class LockWrapper<T>
    {
        private Action<T> action;
        private Func<T, bool> predicate;
        private object locker;
        public LockWrapper(Func<T, bool> predicate, Action<T> action)
        {
            this.action = action;
            this.predicate = predicate;
            locker = new object();
        }

        public Action<T> Instance { get; }

        private void Run(T t)
        {
            if(predicate(t))
            {
                lock (locker)
                {
                    if(predicate(t))
                        action(t);
                }
            }
        }
    }
}
