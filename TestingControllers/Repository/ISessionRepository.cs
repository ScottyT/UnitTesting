using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingControllers.Models;

namespace TestingControllers.Repository
{
    public interface ISessionRepository
    {
        Task<List<SessionModel>> ListAsync();
        Task AddAsync(SessionModel model);
    }
}
