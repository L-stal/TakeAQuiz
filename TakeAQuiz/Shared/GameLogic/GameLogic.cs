using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TakeAQuiz.Shared.ViewModels;
using static System.Net.WebRequestMethods;


namespace TakeAQuiz.Shared.GameLogic
{
    public class GameLogic
    {
        private readonly HttpClient _httpClient;
      
        public GameLogic(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      
        // Game active status
        public bool ActiveGame { get; set; }
        // Question active status
        public bool? ActiveQuestion { get; set; } = null;
        // Game finished status
        public bool FinishedGame { get; set; }
        // Amount of questions in quiz
        public int QAmount { get; set; }
        // Index of current question being asked
        public int QIndex { get; set; }
        // Current score being held by player
        public int CurrentScore { get; set; }
        // Question answer result, correct/incorrect
        public bool? QResult { get; set; } = null;

        public async Task StartGame()
        {
            ActiveGame = true;
        }

        public async Task ResetGame()
        {
            ActiveGame = false;
            ActiveQuestion = null;
            FinishedGame = false;
            QAmount = 0;
            QIndex = 0;
            CurrentScore = 0;
            QResult = null;
        }

        public async Task EndGame(string title, int score)
        {
            await _httpClient.PostAsJsonAsync("api/game/savegame", new { title = title, score = score });
        }

        public async Task MakeGuess(string guess, string answer)
        {
            ActiveQuestion = false;
            if (guess != answer)
            {
                CurrentScore -= 100;
                QResult = false;
            }
            else
            {
                QResult = true;
            }
        }
    }
}

