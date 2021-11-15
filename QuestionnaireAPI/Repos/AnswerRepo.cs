using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class AnswerRepo : GenRepo, IAnswerRepo
    {
        private readonly QuestionnaireDbContext _context;
        public AnswerRepo(QuestionnaireDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<QuestionAnswerClose>> AddCloseAnswer( int questionId, List<QuestionAnswerClose> questionAnswerCloseList)
        {
            var question = _context.Questions.Where(x => x.Id == questionId);
            if(question is null)
            {
                throw new System.NotImplementedException();
            }

            foreach(var questionAnswerClose in questionAnswerCloseList)
            {
                var answer = _context.SubAnswers.Where(x => x.Id == questionAnswerClose.Id );
                if(answer is null)
                {
                    throw new System.NotImplementedException();
                }

            }

            await _context.AddRangeAsync(questionAnswerCloseList);
            await _context.SaveChangesAsync();

            return questionAnswerCloseList;

        }

        public async Task<QuestionAnswerOpen> AddOpenAnswer(int questionId, QuestionAnswerOpen questionAnswerOpen)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if(question is null)
            {
                throw new System.NotImplementedException();
            }
            questionAnswerOpen.QuestionId = questionId;
            await _context.AddAsync(questionAnswerOpen);
            await _context.SaveChangesAsync();

            return questionAnswerOpen;
        }

        public async Task<Answer> AddSubAnswer(int questionId,Answer answer)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if(question is null)
            {
                //Kod do obslugi jakby nie było takiego pytania
                throw new System.Exception();
            }
            answer.QuestionId = question.Id;
            await _context.AddAsync(answer);
            await _context.SaveChangesAsync();
            return answer;


        }
    }
}