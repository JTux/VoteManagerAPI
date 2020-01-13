using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.Motion;

namespace VoteManagerAPI.Contracts
{
    public interface IMotionService : IAuthorizedService
    {
        // CREATE New
        Task<bool> CreateMotionAsync(MotionCreate model);

        // GET By ID
        Task<MotionDetail> GetMotionByIdAsync(int motionId);

        // GET All Motions
        Task<List<MotionDetail>> GetAllMotionsAsync();

        // Update Existing
        Task<bool> UpdateExistingMotionAsync(MotionUpdate model);
    }
}