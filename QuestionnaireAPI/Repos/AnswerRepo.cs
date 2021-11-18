using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class AnswerRepo : GenRepo, IAnswerRepo
    {
        private readonly QuestionnaireDbContext _context;
        public AnswerRepo(QuestionnaireDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<QuestionAnswerClose>> AddCloseAnswer(int questionId, List<QuestionAnswerClose> questionAnswerCloseList)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if (question is null)
            {
                throw new NotFoundException("Question does not exist");
            }

            if (question.QuestionType == QuestionType.Open)
            {
                throw new ValidationException("Wrong question type, this question is not open");
            }

            if (question.QuestionType == QuestionType.Single)
            {
                if (questionAnswerCloseList.Count() == 1)
                {
                    await _context.AddRangeAsync(questionAnswerCloseList);
                    await _context.SaveChangesAsync();
                    return questionAnswerCloseList;
                }
                else
                {
                    throw new ValidationException("Too many answers, question is single");
                }
            }

            foreach (var questionAnswerClose in questionAnswerCloseList)
            {
                var answer = _context.SubAnswers.Where(x => x.Id == questionAnswerClose.Id);
                if (answer is null)
                {
                    throw new NotFoundException("Subanswer does not exist");
                }

            }

            await _context.AddRangeAsync(questionAnswerCloseList);
            await _context.SaveChangesAsync();

            return questionAnswerCloseList;

        }

        public async Task<QuestionAnswerOpen> AddOpenAnswer(int questionId, QuestionAnswerOpen questionAnswerOpen)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if (question is null)
            {
                throw new System.NotImplementedException();
            }
            if (question.QuestionType != QuestionType.Open)
            {
                throw new ValidationException("Wrong question type, this question is not open");
            }
            questionAnswerOpen.QuestionId = questionId;
            await _context.AddAsync(questionAnswerOpen);
            await _context.SaveChangesAsync();

            return questionAnswerOpen;
        }

        public async Task<List<SubAnswer>> AddSubAnswer(int questionId, List<SubAnswer> subAnswers, int userId)
        {

            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if (question is null)
            {
                //Kod do obslugi jakby nie było takiego pytania
                throw new NotFoundException("Question not found");
            }
            var questionnaireOwnerId = await _context.Questionnaires.Where(x => x.Id == question.QuestionnaireId).Select(x => x.UserId).FirstOrDefaultAsync();
            if (questionnaireOwnerId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }
            foreach (var subAnswer in subAnswers)
            {
                subAnswer.QuestionId = questionId;
            }

            await _context.AddRangeAsync(subAnswers);
            await _context.SaveChangesAsync();
            return subAnswers;


        }

        public async Task DeleteSubAnswer(int subAnswerId, int userId)
        {
            var subAnswer = await _context.SubAnswers.FirstOrDefaultAsync(x => x.Id == subAnswerId);
            if (subAnswer is null)
            {
                throw new NotFoundException("Subanswer not found");
            }
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == subAnswer.QuestionId);
            var questionnaireOwnerId = await _context.Questionnaires.Where(x => x.Id == question.QuestionnaireId).Select(x => x.UserId).FirstOrDefaultAsync();
            if (questionnaireOwnerId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }

            _context.Remove(subAnswer);
            await _context.SaveChangesAsync();
        }
    }
}