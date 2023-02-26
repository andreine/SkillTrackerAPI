using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using IronXL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SkillTracker.Dtos;
using System.IdentityModel.Tokens.Jwt;

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


        [HttpGet("GetEmployeeSessionQuestions/{sessionId}")]
        public async Task<IList<QuestionEmployeeDto>> GetEmployeeSessionQuestions(int sessionId)
        {
            var questions = await _questionsService.GetEmployeeSessionQuestions(sessionId);
            return questions;
        }


        [HttpPost("sessionsubmitanswers")]
        public async Task<ActionResult> SessionSubmitAnswers(SubmitedSession submitedSession)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "userId").Value;

            _questionsService.AddSubmitedSession(submitedSession.Answers, userId, submitedSession.SessionId);

            return Ok(new
            {
                userSessionId = submitedSession.SessionId
            });

        }


        [HttpGet("getEmployeeSessionReport/{userSessionId}")]
        public async Task<List<EmployeeSessionReportDto>> GetEmployeeSessionReport(int userSessionId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "userId").Value;

            var report = await _questionsService.GetEmployeeSessionReport(userSessionId, userId);

            return report;
        }


        [HttpGet("getEmployeeQuestionReport/{userSessionId}")]
        public async Task<List<QuestionsReportDto>> GetEmployeeQuestionReport(int userSessionId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "userId").Value;

            var report = await _questionsService.GetEmployeeQuestionReport(userSessionId, userId);

            return report;
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
