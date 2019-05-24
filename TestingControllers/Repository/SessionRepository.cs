using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingControllers.Models;

namespace TestingControllers.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(SessionModel model)
        {
            _context.Sessions.Add(model);
            return _context.SaveChangesAsync();
        }

        public Task<List<SessionModel>> ListAsync()
        {
            return _context.Sessions.ToListAsync();
        }
    }
}
