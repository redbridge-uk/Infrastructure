using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redbridge.DependencyInjection;

namespace Redbridge.EntityFramework
{
    public abstract class WorkUnit<TContext> : IWorkUnit
    where TContext: DbContext, new()
    {
        private readonly TContext _context;
        private readonly ILogger _logger;

        protected ILogger Logger => _logger;
        protected TContext Context => _context;

        protected WorkUnit (TContext context, ILogger<WorkUnit<TContext>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
