using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Quit_med_JSON_serialisering
{

 
        public class QuizQuestion
        {
            public string Question { get; set; }
            public List<string> AnswerOptions { get; set; }
            public int CorrectAnswer { get; set; }

      //  public List<string> ValidNumber_AnswerOptions { get; set; }

        public List<string> CorrectAnswerMessage { get; set; }   // Ny egenskab til den ekstra besked

    }

    public class Quiz
    {
        public List<QuizQuestion> Questions { get; set; }
    }
    
        
}

