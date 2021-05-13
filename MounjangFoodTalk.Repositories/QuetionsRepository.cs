using MounjangFoodTalk.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int QuestionID);


    }
    public class QuestionsRepository : IQuestionsRepository
    {

        private readonly MounjangFoodTalkDatabaseDbContext _mounjangFoodTalkDatabaseDbContext;

        public QuestionsRepository()
        {
            _mounjangFoodTalkDatabaseDbContext = new MounjangFoodTalkDatabaseDbContext();
        }

        public void InsertQuestion(Question q)
        {
            _mounjangFoodTalkDatabaseDbContext.Questions.Add(q);
            _mounjangFoodTalkDatabaseDbContext.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if (qt != null)
            {
                qt.QuestionName = q.QuestionName;
                qt.QuestionDateAndTime = q.QuestionDateAndTime;
                qt.CategoryID = q.CategoryID;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }


        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();

            if (qt != null)
            {
                qt.VotesCount += value;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }


        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();

            if (qt != null)
            {
                qt.AnswersCount += value;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            Question qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();

            if (qt != null)
            {
                qt.ViewsCount += value;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }

        public void DeleteQuestion(int qid)
        {
            Question qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();

            if (qt != null)
            {
                _mounjangFoodTalkDatabaseDbContext.Questions.Remove(qt);
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }


        public List<Question> GetQuestions()
        {
            List<Question> qt = _mounjangFoodTalkDatabaseDbContext.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return qt;
        }


        public List<Question> GetQuestionByQuestionID(int QuestionID)
        {
            List<Question> qt = _mounjangFoodTalkDatabaseDbContext.Questions.Where(temp => temp.QuestionID == QuestionID).ToList();
            return qt;
        }







    }
}
