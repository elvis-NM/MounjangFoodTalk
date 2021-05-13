using MounjangFoodTalk.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int aid, int uid, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        private readonly MounjangFoodTalkDatabaseDbContext _mounjangFoodTalkDatabaseDbContext;

        public VotesRepository()
        {
            _mounjangFoodTalkDatabaseDbContext = new MounjangFoodTalkDatabaseDbContext();
        }

        public void UpdateVote(int aid, int uid, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Vote vote = _mounjangFoodTalkDatabaseDbContext.Votes.Where(temp => temp.AnswerID == aid && temp.UserID == uid).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = aid, UserID = uid, VoteValue = updateValue };
                _mounjangFoodTalkDatabaseDbContext.Votes.Add(newVote);
            }
            _mounjangFoodTalkDatabaseDbContext.SaveChanges();


        }
    }
}
