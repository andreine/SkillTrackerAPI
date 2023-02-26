using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class QuestionsReportDto
    {
        public string QuestionName { get; set; }
        public string QuestionCategory { get; set; }
        public int IsCorrect { get; set; }
    }
}
