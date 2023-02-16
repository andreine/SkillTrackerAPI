using Domain.Entities;

namespace SkillTracker.Dtos
{
    public class QuestionEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public string FourthAnswer { get; set; }
        public int QuestionCategoryId { get; set; }
        public string QuestionCategoryName { get; set; }
    }
}
