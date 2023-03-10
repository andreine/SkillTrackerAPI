using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using NPOI.SS.Formula.Functions;

namespace SkillTracker.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, SessionQuestionsDto>()
                .ForMember(d => d.QuestionCategory, o => o.MapFrom(s => s.QuestionCategory.Name));


       

        }


    }
}
