using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Contracts
{
    public interface IAuthorizedService
    {
        // Set UserId
        void SetUserId(string userId);
    }
}