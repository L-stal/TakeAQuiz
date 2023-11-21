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
        public bool ActiveGame { get; set; }
        public bool FinishedGame { get; set; }
        public int QAmount { get; set; }
        public int QIndex { get; set; }
        public int CurrentScore { get; set; }

        public async Task StartGame()
        {
            ActiveGame = true;
        }


        public async Task MakeGuess(string guess, string answer,string title)
        {
            if (guess != answer)
            {
                CurrentScore -= 100;
            }
            QIndex++;    
            if (QIndex == QAmount)
            {
                await EndGame(title ,CurrentScore);
                FinishedGame = true;
            }

            
        }

        public async Task EndGame(string title, int score)
        {
            await _httpClient.PostAsJsonAsync("api/game/savegame", new { title = title, score = score });
        }
    }
}

