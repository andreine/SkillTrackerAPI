using Domain.Entities;
using Domain.Entities.Identity;
using IronXL;
using SkillTracker.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IQuestionsService
    {
        bool uploadFileToDatabase(WorkBook workbook);
        IList<Question> getAllQuestions ();
        Task<IList<QuestionEmployeeDto>> getAllQuestionsForEmployee();
    }
}
