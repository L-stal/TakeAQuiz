﻿using Microsoft.AspNetCore.Http;
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
                .Where(x => x.Title == request.Title)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var game = new GameModel()
            {
                QuizId = quiz,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Score = request.Score,
            };

            // Todo:
            // Increment games played by 1

            _context.Games.Add(game);
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
