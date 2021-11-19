using System.Collections.Generic;
using AutoMapper;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Questionnaire, QuestionnaireDisplayInListDto>()
            .ForMember(x => x.UserName, x => x.MapFrom(x => x.User.Name));

            CreateMap<Question, QuestionInQuestionnaireDto>()
             .ForMember(x => x.SubAnswers, x => x.MapFrom(x => x.SubAnswers));

            CreateMap<SubAnswer,SubAnswerInQuestionnaireDto>();

            CreateMap<QuestionnaireAddDto, Question>();
            CreateMap<Question, QuestionnaireAddDto>();
           
            
        }
    }
}