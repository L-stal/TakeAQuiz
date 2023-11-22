using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeAQuiz.Shared.ViewModels
{
    public class GameViewModel
    {
        public int QuizId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
