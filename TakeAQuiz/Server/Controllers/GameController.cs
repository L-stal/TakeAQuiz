using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Security.Claims;
using TakeAQuiz.Server.Data;
using TakeAQuiz.Server.Models;
using TakeAQuiz.Shared.ViewModels;

namespace TakeAQuiz.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public GameController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("savegame")]
        public async Task<IActionResult> SaveGame([FromBody] SaveGameRequest request)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.User)
                .Where(x => x.Title == request.Title)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                return NotFound();
            }

            var game = new GameModel()
            {
                QuizId = quiz.Id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Score = request.Score,
            };
            _context.Games.Add(game);

            quiz.GamesPlayed += 1;

            await _context.SaveChangesAsync();

            return Ok();
        }

        public class SaveGameRequest
        {
            public string Title { get; set; }
            public int Score { get; set; }
        }
    }
}
