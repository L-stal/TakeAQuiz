using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.IO.Enumeration;
using System.Net;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QuizController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
                    Id = quiz.Id,
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
            var quiz = _context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.MockAnswers)
                                        .Where(t => t.Title == title)
                                        .FirstOrDefault();
            
            if (quiz == null)
            {
                throw new Exception("Quiz could not be found.");
            }

            var quizView = new QuizViewModel
            {
                Id = quiz.Id,
                Title = quiz.Title,
                GamesPlayed = quiz.GamesPlayed ?? 0,
                MaxScore = quiz.MaxScore,
                OverallRating = quiz.OverallRating ?? 0,
                Questions = quiz.Questions.Select(q => new QuestionViewModel
                {
                    Question = q.Question,
                    Answer = q.Answer,
                    Media = q.Media,
                    TimeLimit = q.TimeLimit,
                    MockAnswers = q.MockAnswers.Select(m => new MockViewModel
                    {
                        MockAnswer = m.MockAnswer
                    }).ToList() ?? new List<MockViewModel>()
                }).ToList() ?? new List<QuestionViewModel>()
            };

            return quizView;
        }

        [HttpPost("createquiz")]
        public async Task<IActionResult> CreateQuiz([FromBody]QuizViewModel quiz)
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

		[HttpPost("transferfiles")]
		public async Task<ActionResult<List<FileUploadViewModel>>> TransferFiles(List<FileUploadViewModel> files)
		{
			try
			{
				foreach (var file in files)
				{
					var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.NewFileName);

					using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
					{
						await new MemoryStream(file.FileBytes).CopyToAsync(fileStream);
					}
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}
	}
}
