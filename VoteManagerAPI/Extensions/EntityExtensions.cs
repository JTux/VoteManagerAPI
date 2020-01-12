using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Session;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Extensions
{
    public static class EntityExtensions
    {
        public static SessionDetail ToDetail(this SessionEntity entity)
        {
            return new SessionDetail
            {
                SessionId = entity.Id,
                StartDate = entity.StartDate,
                CreatorName = entity.Creator.UserName,
                IsActive = entity.IsActive,
                Motions = entity.OrdersOfBusiness.Where(e => e is MotionEntity).Select(e => (MotionDetail)e.ToDetail()).ToList(),
                Amendments = entity.OrdersOfBusiness.Where(e => e is AmendmentEntity).Select(e => (AmendmentDetail)e.ToDetail()).ToList()
            };
        }

        public static OrderOfBusinessDetail ToDetail(this OrderOfBusinessEntity entity)
        {
            if (entity is MotionEntity)
                return ((MotionEntity)entity).ToDetail();

            return ((AmendmentEntity)entity).ToDetail();
        }

        public static MotionDetail ToDetail(this MotionEntity entity)
        {
            return new MotionDetail
            {
                MotionId = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                PresenterName = entity.PresentingUser.UserName,
                OriginalSessionId = entity.OriginalSessionId,
                IsActive = entity.IsActive,
                IsTabled = entity.IsTabled,
                Votes = entity.Votes.Select(v => v.ToDetail()).ToList()
            };
        }

        public static AmendmentDetail ToDetail(this AmendmentEntity entity)
        {
            return new AmendmentDetail
            {
                AmendmentId = entity.Id,
                RuleId = entity.RuleId,
                Title = entity.Title,
                Description = entity.Description,
                PresenterName = entity.PresentingUser.UserName,
                OriginalSessionId = entity.OriginalSessionId,
                IsActive = entity.IsActive,
                IsTabled = entity.IsTabled,
                IsPassed = entity.IsPassed,
                Votes = entity.Votes.Select(v => v.ToDetail()).ToList()
            };
        }

        public static VoteDetail ToDetail(this VoteEntity entity)
        {
            return new VoteDetail
            {
                VoteId = entity.Id,
                OrderOfBusinessId = entity.OrderOfBusinessId,
                VoterName = (entity.IsAnonymous) ? "Anonymous" : entity.Voter.UserName,
                Status = entity.Status
            };
        }
    }
}