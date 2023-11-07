using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using System.Security.Claims;
using TakeAQuiz.Server.Data;
using TakeAQuiz.Server.Models;
using TakeAQuiz.Shared.ViewModels;

namespace TakeAQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("getuserdata")]
        public async Task<ActionResult<UserViewModel>> GetUserData()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var quizzes = _context.Quizzes.Where(id => id.UserId == user.Id).ToList();

                List<QuizViewModel> quizzesView = new List<QuizViewModel>();

                foreach (var quiz in quizzes)
                {
                    var quizView = new QuizViewModel
                    {
                        Title = quiz.Title,
                        MaxScore = quiz.MaxScore,
                        GamesPlayed = quiz.GamesPlayed,
                        OverallRating = quiz.OverallRating,
                    };
                    quizzesView.Add(quizView);
                }

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userInfo = new UserViewModel()
                {
                    UserName = user.UserName,
                    Quizzes = quizzesView
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }
    }
}
