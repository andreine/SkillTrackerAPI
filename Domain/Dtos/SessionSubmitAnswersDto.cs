using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SubmitedAnswer
    {
        public int QuestionId { get; set; }
        public string Answered { get; set; }
    }

    public class SubmitedSession
    {
        public IList<SubmitedAnswer> Answers { get; set; }
        public int SessionId { get; set; }
    }
}
