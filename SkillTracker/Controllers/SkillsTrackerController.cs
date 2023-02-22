using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using IronXL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Dtos;

namespace SkillTracker.Controllers
{
    public class FormQuestionsDto
    {
        public IFormFile File { get; set; }
        public string SessionId { get; set; }
    }
    public class SkillsTrackerController : BaseApiController
    {


        private readonly IQuestionsService _questionsService;

        public SkillsTrackerController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpPost("uploadSkills")]
        public async Task<IActionResult> UploadSkills([FromForm]FormQuestionsDto formInformation)
        {

            WorkBook workBook = WorkBook.LoadExcel(formInformation.File.OpenReadStream());
            _questionsService.uploadFileToDatabase(workBook, formInformation.SessionId);



            return Ok(new
            {
                success = "worked"
            });
        }


        [HttpGet("getsessionquestions/{sessionId}")]
        [Authorize(Policy = "ManageSkills")]
        public async Task<IList<Question>> GetSessionQuestions(string sessionId)
        {
            return _questionsService.getAllSessionQuestions(sessionId);
        }


        [HttpGet("getquestionsemployee")]
        public async Task<IList<QuestionEmployeeDto>> GetQuestionsEmployee()
        {
            var questions = await _questionsService.getAllQuestionsForEmployee();
            return questions;
        }


        [HttpPost("sessionsubmitanswers")]
        public async Task<IActionResult> SessionSubmitAnswers(SessionSubmitAnswersDto sessionSubmitAnswers)
        {
            return Ok("DASD");
        }



        [HttpGet("getAllSessions")]
        [Authorize(Policy = "ManageSkills")]
        public async Task<IList<Session>> GetAllSessions()
        {
            return await _questionsService.GetAllSessions();
        }



        [HttpPost("addNewSession")]
        [Authorize(Policy = "ManageSkills")]
        public async Task<ActionResult> AddNewSession(NewSessionDto newSessionDto)
        {
            var sessionName = await _questionsService.AddNewSession(newSessionDto.Name);

            return Ok(new
            {
                name = sessionName
            });
        }


    }
}
