using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MounjangFoodTalk.DomainModels;
using MounjangFoodTalk.Repositories;
using MounjangFoodTalk.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace MounjangFoodTalk.ServiceLayer
{

    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetail(EditQuestionViewModel qvm);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);

        void UpdateQuestionViewsCount(int qid, int value);

        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);

    }
    public class QuestionsService : IQuestionsService
    {
        IQuestionsRepository qr;

        public QuestionsService()
        {
            qr = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);
        }


        public void UpdateQuestionDetail(EditQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid, value);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCount(qid, value);
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid, value);
        }

        public void DeleteQuestion(int qid)
        {
            qr.DeleteQuestion(qid);
        }


        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> q = qr.GetQuestions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return qvm;
        }

        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID = 0)
        {
            Question q = qr.GetQuestionByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel qvm = null;

            if (q != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Question, QuestionViewModel>(); cfg.ignoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(q);

                foreach (var item in qvm.Answers)
                {

                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();

                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }

            return qvm;

        }

    }
}
