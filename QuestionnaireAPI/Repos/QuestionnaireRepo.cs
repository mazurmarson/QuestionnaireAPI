using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class QuestionnaireRepo : GenRepo, IQuestionnaireRepo
    {
        private readonly QuestionnaireDbContext _context;
        private readonly IMapper _mapper;
        public QuestionnaireRepo(QuestionnaireDbContext context, IMapper mapper) :base(context)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<Questionnaire> AddQuestionnaire(Questionnaire questionnaire, int userId)
        {
          var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user is null)
            {
                throw new NotFoundException("This user does not exist");
            }
           questionnaire.User = user;
           questionnaire.CreateDate = System.DateTime.Now;
            await _context.AddAsync(questionnaire);
            await _context.SaveChangesAsync();

            return questionnaire;
        }

        public async Task DeleteQuestionnaire(int questionnaireId, int userId)
        {
            var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new NotFoundException("This questionnaire does not exist");
            }
            if(questionnaire.UserId != userId)
            {
                throw new UnauthorizedException("This resource is not your");
            }

             _context.Remove(questionnaire);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<QuestionnaireDisplayInListDto>> GetQuestionnaires()
        {
            var questionnaries = await _context.Questionnaires.Include(x => x.User).ToListAsync();

            var result = _mapper.Map<List<QuestionnaireDisplayInListDto>>(questionnaries);
            return result;
        }

        public async Task<QuestionnaireDetailsDto> GetQuestionnaire(int questionnaireId)
        {
           
            // var userName =  await _context.Questionnaires.Where(x => x.Id == questionnaireId).Include(x => x.User).Select(x => x.User.Name).FirstOrDefaultAsync();
            // var questionnare = await _context.Questionnaires.Where(x => x.Id == questionnaireId).FirstOrDefaultAsync();
            // var question = await _context.Questions.Where(x => x.QuestionnaireId == questionnaireId).Include(x => x.SubAnswers).ToListAsync();

           
            //   QuestionnaireDetailsDto questionnaireDetailsDto = new QuestionnaireDetailsDto()
            //   {
            //       UserName = userName,
            //       Questions = question
            //   };         

            var questionnaireDetailsDto = await _context.Questionnaires.Where(x => x.Id == questionnaireId)
            .Include(x => x.User)
            .Include(x => x.Questions).Select(x => new QuestionnaireDetailsDto{
                UserName = x.User.Name,
                Id = x.Id,
                CreateDate = x.CreateDate,
                Name = x.Name,
              //  Questions = (_mapper.Map<List<QuestionInQuestionnaireDto>>(x.Questions)).Select(x => )
              Questions = x.Questions.Select(x => new QuestionInQuestionnaireDto {
                  Id = x.Id,
                  QuestionType = x.QuestionType,
                  QuestionContent = x.QuestionContent,
                  SubAnswers = x.SubAnswers.Select(z => new SubAnswerInQuestionnaireDto {
                      Id = z.Id,
                      Content = z.Content
                  }).ToList()
              }).ToList()
            }).FirstOrDefaultAsync();

            // foreach(var question in questionnaireDetailsDto.Questions )
            // {
            //     var subanswers = _context.SubAnswers.Where(x => x.QuestionId == question.Id).ToList();
            //     var subAnswerInQuestionnaireDto = _mapper.Map<SubAnswerInQuestionnaireDto>(subanswers);
            //     question.SubAnswers.Add(subAnswerInQuestionnaireDto);
            // }


           // var result = _mapper.Map<Questionnaire, QuestionnaireDetailsDto>(questionnaire);
            return questionnaireDetailsDto;
        }

        public async Task<QuestionnaireResultsDto> GetQuestionnaireResults(int questionnaireId)
        {
            QuestionnaireResultsDto questionnaireResultsDto = await _context.Questionnaires.Where(x => x.Id == questionnaireId).Select(x => new QuestionnaireResultsDto{
                Id = x.Id,
                Name = x.Name,
                CreateDate = x.CreateDate
            }).FirstOrDefaultAsync();
       
            var questionnaireOpenQuestionsResults = await _context.Questions.Where(x => x.QuestionnaireId == questionnaireId && x.QuestionType == QuestionType.Open)
            .Include(x => x.OpenQuestionAnswerList).Select(x => new QuestionInQuestionnaireResultsOpenDto{
                Id = x.Id,
                QuestionType = x.QuestionType,
                QuestionContent = x.QuestionContent,
                OpenAnswers = x.OpenQuestionAnswerList.Select(z => new QuestionAnswerOpen {
                    Id = z.Id,
                    AnswerContent = z.AnswerContent
                }).ToList()
            }).ToListAsync();

            var questionnaireClosedQuestionsResults = await _context.Questions.Where(x => x.QuestionnaireId == questionnaireId && x.QuestionType != QuestionType.Open)
            .Include(x => x.SubAnswers).ThenInclude(x => x.QuestionAnswerCloseList).Select(x => new QuestionInQuestionnaireResultsCloseDto{
                Id = x.Id,
                QuestionContent = x.QuestionContent,
                QuestionType = x.QuestionType,
                SubAnswers = x.SubAnswers.Select(z => new SubAnswerInQuestionnaireResultDto{
                    Id = z.Id,
                    Content = z.Content,
                    AmountOfAnswers = z.QuestionAnswerCloseList.Count()
                }).ToList()
            }).ToListAsync();

            questionnaireResultsDto.OpenQuestionResults = questionnaireOpenQuestionsResults;
            questionnaireResultsDto.CloseQuestionsResults = questionnaireClosedQuestionsResults;

            return questionnaireResultsDto;

        }
    }
}