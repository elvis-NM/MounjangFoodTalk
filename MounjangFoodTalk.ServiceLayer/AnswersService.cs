using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MounjangFoodTalk.DomainModels;
using MounjangFoodTalk.ViewModels;
using MounjangFoodTalk.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace MounjangFoodTalk.ServiceLayer
{
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);

        void UpdateAnswerVotesCount(int aid, int uid, int value);

        void DeleteAnswer(int aid);
        List<AnswerViewModel> GetAnswersByQuestionID(int qid);
        AnswerViewModel GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersService : IAnswersRepository
    {
        private readonly AnswersRepository ar;

        public AnswersService()
        {
            ar = new AnswersRepository();
        }



        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            ar.InsertAnswer(a);
        }



        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            ar.UpdateAnswer(a);
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }

        public void DeleteAnswer(int aid)
        {
            ar.DeleteAnswer(aid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int qid)
        {
            List<Answer> a = ar.GetAnswersByQuestionID(qid);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.ignoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return avm;
        }


        public AnswerViewModel GetAnswersByAnswerID(int AnswerID)
        {
            Answer a = ar.GetAnswersByQuestionID(AnswerID).FirstOrDefault();
            AnswerViewModel avm = null;

            if (a != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Answer, AnswerViewModel>(); cfg.ignoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }
            return avm;
        }

        public void InsertAnswer(Answer a)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnswer(Answer a)
        {
            throw new NotImplementedException();
        }

        List<Answer> IAnswersRepository.GetAnswersByQuestionID(int qid)
        {
            throw new NotImplementedException();
        }

        List<Answer> IAnswersRepository.GetAnswersByAnswerID(int AnswerID)
        {
            throw new NotImplementedException();
        }
    }
}
