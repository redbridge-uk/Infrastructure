using Microsoft.EntityFrameworkCore;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;

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
            logger.WriteDebug($"Creating new {typeof(TContext).Name} context instance.");
            return new TContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
