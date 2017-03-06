namespace System.Threading.Tasks
{
    public static class AsyncHelper
    {
        public static T WaitForResult<T>(this Task<T> task)
        {
            task.Wait();
            return task.IsFaulted
                ? throw task.Exception
                : task.IsCanceled ? default(T)
                    : task.IsCompleted
                        ? task.Result : default(T);
        }

        async public static Task RunAsync(this Action action)
            => await Task.Factory.StartNew(action).ConfigureAwait(false);

        async public static Task<T> RunAsync<T>(this Func<T> func)
            => await Task.Factory.StartNew(func).ConfigureAwait(false);

        public static Func<Task> ToAsync(this Action action)
            => () => Task.Factory.StartNew(action);

        public static Func<T,Task> ToAsync<T>(this Action<T> action)
            => async x => await Task.Run(() => action(x)).ConfigureAwait(false);

        public static Func<TSource,Task<TResult>> ToAsync<TSource,TResult>(this Func<TSource,TResult> func)
            => async x => await Task.Run(() => func(x)).ConfigureAwait(false);

        async public static Task<TResult> Then<TSource, TResult>(this Task<TSource> task, Func<TSource, TResult> then)
            => await task.ContinueWith(t => then(t.Result)).ConfigureAwait(true);

        async public static Task<TResult> Then<TSource, TResult>(this Task<TSource> task, Func<Task<TSource>, TResult> then)
            => await task.ContinueWith(then).ConfigureAwait(true);
        
        public static Func<T, Task> Empty<T>() => x => Task.CompletedTask;
    }
}
