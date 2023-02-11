﻿using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using IronXL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Dtos;

namespace SkillTracker.Controllers
{
    public class SkillsTrackerController : BaseApiController
    {


        private readonly IQuestionsService _questionsService;

        public SkillsTrackerController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpPost("uploadSkills")]
        public async Task<IActionResult> UploadSkills(IFormFile file)
        {

            WorkBook workBook = WorkBook.LoadExcel(file.OpenReadStream());
            _questionsService.uploadFileToDatabase(workBook);



            return Ok("Hey");
        }


        [HttpGet("getquestions")]
        public async Task<IList<Question>> GetQuestions()
        {
            return _questionsService.getAllQuestions();
        }


    }
}