using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class EmployeeSessionReportDto
    {
        public string CategoryName { get; set; }
        public float FinalScore { get; set; }
    }


    public class CategoryQuestionAnswer
    {
        public string CategoryName { get; set; }
        public int IsCorrect { get; set; }
    }
}
