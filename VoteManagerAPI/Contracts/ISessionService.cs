using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.Session;

namespace VoteManagerAPI.Contracts
{
    public interface ISessionService : IAuthorizedService
    {
        // CREATE
        Task<bool> StartNewSessionAsync();

        // GET Current
        Task<int> GetCurrentSessionIdAsync();

        // GET Session List
        Task<List<SessionListItem>> GetSessionListAsync();

        // GET All
        Task<List<SessionDetail>> GetAllSessionsAsync();

        // GET by ID
        Task<SessionDetail> GetSessionByIdAsync(int sessionId);

        // UPDATE Existing

        // End Current Session
        Task<bool> EndSessionAsync(int sessionId);

        // DELETE Existing by ID
        Task<bool> DeleteSessionByIdAsync(int sessionId);
    }
}