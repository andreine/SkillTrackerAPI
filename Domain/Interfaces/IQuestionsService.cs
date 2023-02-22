using Domain.Dtos;
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
        bool uploadFileToDatabase(WorkBook workbook, string sessionId);
        IList<Question> getAllSessionQuestions (string sessionId);
        Task<IList<QuestionEmployeeDto>> getAllQuestionsForEmployee();
        Task<string> AddNewSession(string sessionName);
        Task<IList<Session>> GetAllSessions();
    }
}
