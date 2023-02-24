using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using IronXL;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QuestionsService: IQuestionsService
    {

        ApplicationDbContext _context;
        public QuestionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> getAllSessionQuestions(string sessionId)
        {
            return _context.Questions.Where(x => Int32.Parse(sessionId) == x.Session.Id).ToList();
        }

        public void AddSubmitedSession(IList<SubmitedAnswer> answers, string userId, int userSessionId)
        {

            foreach (var answer in answers)
            {
                var answeredQuestion = new UserSessionQuestion();
                var userSession = _context.UserSessions.Where(x => x.Id == userSessionId).FirstOrDefault();
                var question = _context.Questions.Include(x => x.Session).Where(x => x.Id == answer.QuestionId).FirstOrDefault();
                if(question.CorrectAnswer == answer.Answered)
                {
                    answeredQuestion.IsCorrect = 1;
                }
                else
                {
                    answeredQuestion.IsCorrect = 0;
                }
                answeredQuestion.UserId = userId;
                answeredQuestion.QuestionId = answer.QuestionId;
                answeredQuestion.UserSession = userSession;
                _context.UserSessionQuestions.Add(answeredQuestion);
            }
            var employeeSession = _context.UserSessions.Where(x => x.Id == userSessionId).FirstOrDefault();
            employeeSession.IsCompleted = 1;
            _context.SaveChanges();

        }


        public async Task<List<EmployeeSessionReportDto>> GetEmployeeSessionReport(int userSessionId, string userId)
        {
            var session = _context.UserSessions.Include(x => x.Session).Where(x => x.Id == userSessionId).FirstOrDefault();
            var questionsWithAnswers = _context.Questions.Include(x => x.Session).Include(x => x.QuestionCategory).Where(x => x.Session.Id == session.Session.Id);
            var userSessionAnswers = await _context.UserSessionQuestions.Include(x => x.UserSession).Where(x => x.UserSession.Id == userSessionId).ToListAsync();


            var categoriesQuestionAnswers = new List<CategoryQuestionAnswer>();
            foreach (var question in userSessionAnswers)
            {
                
                var questionCategory = questionsWithAnswers.Where(x => x.Id == question.QuestionId).Select(x => x.QuestionCategory.Name).FirstOrDefault();
                categoriesQuestionAnswers.Add(new CategoryQuestionAnswer
                {
                    CategoryName = questionCategory,
                    IsCorrect = question.IsCorrect,
                });

            }

            var finalResult = new List<EmployeeSessionReportDto>();

            foreach (var x in categoriesQuestionAnswers)
            {
                var exists = finalResult.Where(final => final.CategoryName == x.CategoryName).FirstOrDefault();
                if (exists == null)
                {
                    finalResult.Add(new EmployeeSessionReportDto
                    {
                        CategoryName = x.CategoryName,
                        FinalScore = x.IsCorrect
                    });
                }
                else
                {
                    exists.FinalScore = (exists.FinalScore + x.IsCorrect) / 2;
                }
            }


            return finalResult;
        }




        public async Task<IList<QuestionEmployeeDto>> GetEmployeeSessionQuestions(int userSessionId)
        {
            var userSession = _context.UserSessions.Include(x => x.Session).Where(x => x.Id == userSessionId).FirstOrDefault();

            var questions =  await _context.Questions.Where(x => x.Session.Id == userSession.Session.Id).Include(x => x.QuestionCategory).Select(x => new QuestionEmployeeDto
            {
                Id = x.Id,
                Name = x.Name,
                FirstAnswer = x.FirstAnswer,
                FourthAnswer = x.FourthAnswer,
                SecondAnswer = x.SecondAnswer,
                ThirdAnswer = x.ThirdAnswer,
                QuestionCategoryId = x.QuestionCategory.Id,
                QuestionCategoryName = x.QuestionCategory.Name
            }).ToListAsync();
            return questions;
        }

        public bool uploadFileToDatabase(WorkBook workbook, string sessionId)
        {
            var workSheet = workbook.WorkSheets[0];

            var rowNumber = workSheet.Rows.Length;


            for (var i = 2; i <= rowNumber; i++)
            {
                var data = workSheet.GetRange($"A{i}:G{i}").ToList();
                var newQuestion = new Domain.Entities.Question();

                var existingCategory = _context.QuestionCategories.FirstOrDefault(x => x.Name == data[0].Text);
                if (existingCategory == null)
                {
                    var newQuestionCategory = new Domain.Entities.QuestionCategory
                    {
                        Name = data[0].Text,
                    };
                    var sessionToAdd = _context.Sessions.FirstOrDefault(x => x.Id == Int32.Parse(sessionId));
                    newQuestion = new Domain.Entities.Question
                    {
                        Name = data[1].Text,
                        FirstAnswer = data[2].Text,
                        SecondAnswer = data[3].Text,
                        ThirdAnswer = data[4].Text,
                        FourthAnswer = data[5].Text,
                        CorrectAnswer = data[6].Text,
                        QuestionCategory = newQuestionCategory,
                        Session = sessionToAdd
                    };
                    _context.QuestionCategories.Add(newQuestionCategory);

                }
                else
                {
                    var sessionToAdd = _context.Sessions.FirstOrDefault(x => x.Id == Int32.Parse(sessionId));

                    newQuestion = new Domain.Entities.Question
                    {
                        Name = data[1].Text,
                        FirstAnswer = data[2].Text,
                        SecondAnswer = data[3].Text,
                        ThirdAnswer = data[4].Text,
                        FourthAnswer = data[5].Text,
                        CorrectAnswer = data[6].Text,
                        QuestionCategory = existingCategory,
                        Session = sessionToAdd

                    };

                }
                _context.Questions.Add(newQuestion);

            }
            _context.SaveChanges();
            return true;
        }


        public async Task<string> AddNewSession(string sessionName)
        {
            var session = new Session
            {
                Created = DateTime.Now,
                Name = sessionName
            };

            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return sessionName;
        }



        public async Task<IList<Session>> GetAllSessions()
        {
            return await _context.Sessions.ToListAsync();
        }





    }
}
