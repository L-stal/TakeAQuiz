using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Differencing;
using System.Security.Claims;
using TakeAQuiz.Server.Data;
using TakeAQuiz.Server.Models;
using TakeAQuiz.Shared.ViewModels;
using static MudBlazor.CategoryTypes;

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

        [HttpGet("getallquizzes")]
        public async Task<List<QuizViewModel>> GetAllQuizzes()
        {
            var quizzes = _context.Quizzes.ToList();
            
            List<QuizViewModel> quizzesView = new List<QuizViewModel>();

            foreach (var quiz in quizzes)
            {
                var quizView = new QuizViewModel
                {
                    Title = quiz.Title,
                    MaxScore = quiz.MaxScore,
                    GamesPlayed = quiz.GamesPlayed,
                    OverallRating = quiz.OverallRating
                };
                quizzesView.Add(quizView);
            }

            return quizzesView;
        }

        [HttpGet("getquiz/{title}")]
        public async Task<QuizViewModel> GetQuiz(string title)
        {
            var quiz = _context.Quizzes.Where(t => t.Title == title).FirstOrDefault();

            var quizView = new QuizViewModel
            {
                Title = quiz.Title,
                GamesPlayed = quiz.GamesPlayed,
                MaxScore = quiz.MaxScore,
                OverallRating = quiz.OverallRating,
                Questions = new List<QuestionViewModel>()
            };

            foreach (var question in quiz.Questions)
            {
                var questionsView = new QuestionViewModel
                {
                    Question = question.Question,
                    Answer = question.Answer,
                    Media = question.Media,
                    TimeLimit = question.TimeLimit,
                    MockAnswers = new List<MockViewModel>()
                };

                foreach (var mock in question.MockAnswers)
                {
                    var mockView = new MockViewModel
                    {
                        MockAnswer = mock.MockAnswer
                    };
                    questionsView.MockAnswers.Add(mockView);

                }
                quizView.Questions.Add(questionsView);
            }

            return quizView;
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
                    TimeLimit = item.TimeLimit,
                    MockAnswers = new List<MockModel>(),
                };
                foreach (var mocks in item.MockAnswers)
                {
                    var mock = new MockModel
                    {
                        QuestionId = question.Id,
                        MockAnswer = mocks.MockAnswer,
                    };

                    question.MockAnswers.Add(mock);
                    _context.MockAnswers.Add(mock);
                }
                _context.Questions.Add(question);
            }

            _context.SaveChanges();
            return Ok(new { Message = "Quiz created successfully!" });
        }

    }
}
