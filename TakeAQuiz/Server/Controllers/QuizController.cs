using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TakeAQuiz.Server.Data;
using TakeAQuiz.Server.Models;
using TakeAQuiz.Shared.ViewModels;

namespace TakeAQuiz.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuizController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost("createquiz")]
        public async Task<IActionResult> CreatQuiz([FromBody] QuizViewModel quiz)
        {
            var quizObject = new QuizModel
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Title = quiz.Title,
                MaxScore = quiz.Questions.Count() * 100,
                GamesPlayed = 0,
                OverallRating = 0,
            };
            _context.Quizzes.Add(quizObject);
            _context.SaveChanges();

            foreach (var item in quiz.Questions)
            {
                var question = new QuestionModel
                {
                    Question = item.Question,
                    Answer = item.Answer,
                    Media = item.Media,
                    QuizId = quizObject.Id,

                };
                _context.Questions.Add(question);
            }
            _context.SaveChanges();
            return Ok(new { Message = "Quiz created successfully!" });
        }

    }
}
