using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redbridge.DependencyInjection;

namespace Redbridge.EntityFrameworkCore
{
    public abstract class WorkUnit<TContext> : IWorkUnit
    where TContext: DbContext, new()
    {
        private readonly TContext _context;
        private readonly ILogger _logger;

        protected ILogger Logger => _logger;
        protected TContext Context => _context;

        protected WorkUnit (ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = CreateContext(logger);
        }

        protected virtual TContext CreateContext(ILogger logger)
        {
            logger.LogDebug($"Creating new {0} context instance.", typeof(TContext).Name);
            return new TContext();
        }
        
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
