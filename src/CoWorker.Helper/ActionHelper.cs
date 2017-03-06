using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class ActionHelper
    {
        public static Action<T> Then<T>(this Action<T> action, Action<T> next)
            => t =>
            {
                action(t);
                next(t);
            };

        public static Func<T, Task> Then<T>(this Func<T, Task> func, Func<T, Task> add)
            => async ctx =>
            {
                await func(ctx);
                await add(ctx);
            };
        
        public static RequestDelegate Then(this RequestDelegate req, RequestDelegate add)
            => async ctx =>
            {
                await req(ctx);
                await add(ctx);
            };

        public static Func<T, T> Then<T>(this Func<T, T> last, Func<T, T> next)
            => t => last(next(t));

        public static Func<Func<T, Task>, Func<T, Task>> Merge<T>(
            this Func<Func<T, Task>, Func<T, Task>> basemiddleware,
            Func<Func<T, Task>, Func<T, Task>> other
        )
            => next => other(basemiddleware(next));

        public static Action<T> Empty<T>() =>
            x =>
            {
            };

        public static Action<T> EmptyWithException<T>(Func<Exception> exgetter) =>
            x => { throw exgetter(); };
    }
}
