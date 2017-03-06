namespace CoWorker.Helper
{
    using System;

    public static class ExceptionHelper
    {
        public readonly static string CurrentValue = nameof(CurrentValue);

        public static TException Throw<TException>(this object val, params object[] args)
            where TException : Exception
        {
            var ex = Activator.CreateInstance(typeof(TException), args) as TException;
            ex.Data.Add(nameof(CurrentValue), val);
            return ex;
        }
    }
}
