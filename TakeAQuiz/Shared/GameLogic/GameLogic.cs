using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TakeAQuiz.Shared.ViewModels;

namespace TakeAQuiz.Shared.GameLogic
{
    public class GameLogic
    {
        public bool ActiveGame { get; set; }
        public bool FinishedGame { get; set; }
        public int QAmount { get; set; }
        public int QIndex { get; set; }
        public int CurrentScore { get; set; }

        public async Task StartGame()
        {
            ActiveGame = true;
        }


        public async Task MakeGuess(string guess, string answer)
        {
            if (guess != answer)
            {
                CurrentScore = -100;
            }
            if (QIndex == QAmount)
            {
                FinishedGame = true;
                EndGame();
            }
             QIndex++;
        }

        public async Task EndGame()
        {
            // API ANROP
        }
    }
}
