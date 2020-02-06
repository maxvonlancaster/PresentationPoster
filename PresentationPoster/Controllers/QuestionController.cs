using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramAccess.Entities;
using TelegramAccess.Repositories.Interfaces;

namespace PresentationPoster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        [Route("getQuestions")]
        public async Task<IActionResult> GetQuestions() 
        {
            var questions = await _questionRepository.GetAll();
            return new JsonResult(questions);
        }

        [HttpGet]
        [Route("getQuestion")]
        public async Task<IActionResult> GetQuestion([FromBody] int i) 
        {
            var question = await _questionRepository.Get(i);
            return new JsonResult(question);
        }

        [HttpPost]
        [Route("postQuestion")]
        public async Task<IActionResult> PostQuestion([FromBody] Question question) 
        {
            await _questionRepository.Create(question);
            return new JsonResult(true);
        }


    }
}