using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Helpers;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class QuestionRepo : GenRepo, IQuestionRepo
    {
        private readonly QuestionnaireDbContext _context;
        private readonly IMapper _mapper; 

        public QuestionRepo(QuestionnaireDbContext context, IMapper mapper):base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Question> AddQuestion(int questionnaireId, QuestionAddDto questionAddDto, int userId) 
        {
            var questionnaire = _context.Questionnaires.FirstOrDefault(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new NotFoundException("Not found questionnaire");
            }
            if(questionnaire.UserId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your questionnaire");
            }
            var question = new Question{
                QuestionType = questionAddDto.QuestionType,
                QuestionContent = questionAddDto.QuestionContent,
                QuestionnaireId = questionnaireId,
                SubAnswers = _mapper.Map<List<SubAnswer>>(questionAddDto.SubAnswers),
           //     OpenQuestionAnswerList = questionAddDto.OpenQuestionAnswerList
            };

            await _context.AddAsync(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task DeleteQuestion(int questionId, UserIdAndRole userIdAndRole)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if(question is null)
            {
                throw new NotFoundException("Not found question");
            }
            var questionnaireOwnerId = await _context.Questionnaires.Where( x=> x.Id == question.QuestionnaireId ).Select(x => x.UserId).FirstOrDefaultAsync();
            if(questionnaireOwnerId != userIdAndRole.UserId && userIdAndRole.UserType != UserType.Admin)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }

             _context.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}