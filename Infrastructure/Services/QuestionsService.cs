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

        public IList<Question> getAllQuestions()
        {
            return _context.Questions.ToList();
        }

        public async Task<IList<QuestionEmployeeDto>> getAllQuestionsForEmployee()
        {
            var questions =  await _context.Questions.Include(x => x.QuestionCategory).Select(x => new QuestionEmployeeDto
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

        public bool uploadFileToDatabase(WorkBook workbook)
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
                    newQuestion = new Domain.Entities.Question
                    {
                        Name = data[1].Text,
                        FirstAnswer = data[2].Text,
                        SecondAnswer = data[3].Text,
                        ThirdAnswer = data[4].Text,
                        FourthAnswer = data[5].Text,
                        CorrectAnswer = data[6].Text,
                        QuestionCategory = newQuestionCategory
                    };
                    _context.QuestionCategories.Add(newQuestionCategory);

                }
                else
                {
                    newQuestion = new Domain.Entities.Question
                    {
                        Name = data[1].Text,
                        FirstAnswer = data[2].Text,
                        SecondAnswer = data[3].Text,
                        ThirdAnswer = data[4].Text,
                        FourthAnswer = data[5].Text,
                        CorrectAnswer = data[6].Text,
                        QuestionCategory = existingCategory
                    };
                }
                _context.Questions.Add(newQuestion);

            }
            _context.SaveChanges();
            return true;
        }
    }
}
