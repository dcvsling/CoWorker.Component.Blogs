namespace CoWorker.Middlewarer
{
    using Abstractions;

    /// <summary>
    /// this class is use to make two TService in different type 
    /// </summary>
    /// <typeparam name="TService">service type</typeparam>
    /// <typeparam name="TMiddleware">middleware type</typeparam>
    internal class MiddlewarerOptions<TService,TMiddleware> : IMiddlewarerOptions<TService>
        where TMiddleware : class
        where TService : class
    {
        /// <summary>
        /// cache arg
        /// </summary>
        private TMiddleware middleware;

        /// <summary>
        /// constructor for middleware
        /// </summary>
        /// <param name="middleware">middleware object</param>
        public MiddlewarerOptions(TMiddleware middleware)
        {
            this.middleware = middleware;
        }

        /// <summary>
        /// return service with middleware
        /// </summary>
        TService IMiddlewarerOptions<TService>.Value => middleware as TService;
    }
}