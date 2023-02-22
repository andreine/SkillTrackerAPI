using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SessionSubmitAnswersDto
    {
        IList<SubmitedAnswer> submitedAnswers;
    }

    public class SubmitedAnswer
    {
        public string QuestionId { get; set; }
        public string Answered { get; set; }
    }
}
