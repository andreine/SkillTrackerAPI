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
        Task<IList<QuestionEmployeeDto>> GetEmployeeSessionQuestions(int sessionId);
        Task<string> AddNewSession(string sessionName);
        Task<IList<Session>> GetAllSessions();
        void AddSubmitedSession(IList<SubmitedAnswer> answers, string userId, int sessionId);
        Task<List<EmployeeSessionReportDto>> GetEmployeeSessionReport(int sessionId, string userId);
        Task<List<QuestionsReportDto>> GetEmployeeQuestionReport(int userSessionId, string userId);
    }
}
