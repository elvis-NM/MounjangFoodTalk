using MounjangFoodTalk.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.Repositories
{

    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionID(int qid);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        private readonly MounjangFoodTalkDatabaseDbContext _mounjangFoodTalkDatabaseDbContext;
        private readonly IQuestionsRepository qr;
        private readonly IVotesRepository vr;

        public AnswersRepository()
        {
            _mounjangFoodTalkDatabaseDbContext = new MounjangFoodTalkDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }




        public void InsertAnswer(Answer a)
        {
            _mounjangFoodTalkDatabaseDbContext.Answers.Add(a);
            _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }


        public void UpdateAnswer(Answer a)
        {
            Answer ans = _mounjangFoodTalkDatabaseDbContext.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if (ans != null)
            {
                ans.AnswerText = a.AnswerText;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            }
        }


        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = _mounjangFoodTalkDatabaseDbContext.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount += value;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionID, value);
                vr.UpdateVote(aid, uid, value);
            }
        }


        public void DeleteAnswer(int aid)
        {
            Answer ans = _mounjangFoodTalkDatabaseDbContext.Answers.Where(temp => temp.AnswerID == aid).First();

            if (ans != null)
            {
                _mounjangFoodTalkDatabaseDbContext.Answers.Remove(ans);
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }


        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans = _mounjangFoodTalkDatabaseDbContext.Answers.Where(temp => temp.QuestionID == qid).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return ans;
        }


        public List<Answer> GetAnswersByAnswerID(int aid)
        {
            List<Answer> ans = _mounjangFoodTalkDatabaseDbContext.Answers.Where(temp => temp.AnswerID == aid).ToList();
            return ans;
        }






    }
}

