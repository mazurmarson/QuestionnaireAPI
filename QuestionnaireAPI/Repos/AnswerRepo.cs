using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Helpers;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class AnswerRepo : GenRepo, IAnswerRepo
    {
        private readonly QuestionnaireDbContext _context;
        private readonly IMapper _mapper;
        public AnswerRepo(QuestionnaireDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<QuestionAnswerClose>> AddCloseAnswer(int questionId, List<QuestionAnswerCloseAddDto> questionAnswerCloseAddDto)
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
            var questionAnswerCloseList = _mapper.Map<List<QuestionAnswerClose>>(questionAnswerCloseAddDto);
            var subAnswers = await _context.SubAnswers.Where(x => x.QuestionId == questionId).ToListAsync();
            
            bool wrongSubAnswersId = false;
            questionAnswerCloseList = questionAnswerCloseList.GroupBy(x => x.SubAnswerId).Select(z => z.FirstOrDefault()).ToList();
            foreach(var closeAnswer in questionAnswerCloseList)
            {
                if(!(subAnswers.Select(x => x.Id).Contains(closeAnswer.SubAnswerId)))
                    wrongSubAnswersId = true;

                System.Console.WriteLine(closeAnswer);
                
            }

            if(wrongSubAnswersId == true)
            {
                throw new ValidationException("Some subanswer is not from this question");
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

        public async Task<QuestionAnswerOpen> AddOpenAnswer(int questionId, QuestionAnswerContentAddDto questionAnswerContentAddDtoValidator)
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
            var openAnswer = new QuestionAnswerOpen{
                QuestionId = question.Id,
                AnswerContent = questionAnswerContentAddDtoValidator.AnswerContent
            };
            
            await _context.AddAsync(openAnswer);
            await _context.SaveChangesAsync();

            return openAnswer;
        }

        public async Task<List<SubAnswer>> AddSubAnswer(int questionId, List<SubAnswerAddDto> subAnswersAddDto, int userId)
        {
            List<SubAnswer> subAnswers = new List<SubAnswer>();
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if (question is null)
            {
                
                throw new NotFoundException("Question not found");
            }
            if(question.QuestionType == QuestionType.Open)
            {
                throw new ValidationException("Can not add subanswers, question type is open");
            }
            var questionnaireOwnerId = await _context.Questionnaires.Where(x => x.Id == question.QuestionnaireId).Select(x => x.UserId).FirstOrDefaultAsync();
            if (questionnaireOwnerId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }
            foreach (var subAnswer in subAnswersAddDto)
            {
                subAnswers.Add(new SubAnswer{
                    QuestionId = questionId,
                    Content = subAnswer.Content
                }
                );

            }

            await _context.AddRangeAsync(subAnswers);
            await _context.SaveChangesAsync();
            return subAnswers;


        }

        public async Task DeleteSubAnswer(int subAnswerId, UserIdAndRole userIdAndRole)
        {
            var subAnswer = await _context.SubAnswers.FirstOrDefaultAsync(x => x.Id == subAnswerId);
            if (subAnswer is null)
            {
                throw new NotFoundException("Subanswer not found");
            }
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == subAnswer.QuestionId);
            var questionnaireOwnerId = await _context.Questionnaires.Where(x => x.Id == question.QuestionnaireId).Select(x => x.UserId).FirstOrDefaultAsync();

            if (questionnaireOwnerId != userIdAndRole.UserId && userIdAndRole.UserType != UserType.Admin)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }

            _context.Remove(subAnswer);
            await _context.SaveChangesAsync();
        }
    }
}