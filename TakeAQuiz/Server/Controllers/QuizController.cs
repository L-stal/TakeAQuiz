using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TakeAQuiz.Server.Data;
using TakeAQuiz.Server.Models;

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

    }
}
