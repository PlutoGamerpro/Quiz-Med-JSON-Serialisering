
using System.Text.Json;

namespace Quit_med_JSON_serialisering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();


        }
        static Quiz LoadQuizFromFile(string filePath)
        {
            // Læs JSON-filens indhold
            string jsonString = File.ReadAllText(filePath);

            // Deserialiser JSON-strengen til et Quiz-objekt
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(jsonString);

            return quiz;
        }

        static void RunQuiz(Quiz quiz)
        {
            int totalQuestions = quiz.Questions.Count;
            int score = 0;

            for (int currentQuestionIndex = 0; currentQuestionIndex < totalQuestions; currentQuestionIndex++)
            {
                var question = quiz.Questions[currentQuestionIndex];
                Console.WriteLine(question.Question);

                // Vis svarmulighederne med stjerner (*)
                foreach (var option in question.AnswerOptions)
                {
                    Console.WriteLine($"* {option}");
                }

                // Læs brugerens svar
                Console.Write("Indtast dit svar: ");
                string userInput = Console.ReadLine()?.Trim();

                // Find det korrekte svar
                string correctAnswer = question.AnswerOptions[question.CorrectAnswer];

                // Check om brugerens input er lig med det korrekte svar
                if (userInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nKorrekt!\n");

                    // Vis den korrekte svarbesked
                    if (question.CorrectAnswerMessage != null && question.CorrectAnswerMessage.Count > 0)
                    {
                        foreach (var message in question.CorrectAnswerMessage)
                        {
                            Console.WriteLine(message);
                        }
                    }
                    score++;
                }
                else
                {
                    Console.WriteLine($"\nForkert. Det rigtige svar var: {correctAnswer}\n");

                    if (question.CorrectAnswerMessage != null && question.CorrectAnswerMessage.Count > 0)
                    {
                        foreach (var message in question.CorrectAnswerMessage)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }

                // Check om det er sidste spørgsmål
                if (currentQuestionIndex == totalQuestions - 1)
                {
                    Console.WriteLine("\nQuizzen er slut! Tryk på en tast for at afslutte.");
                }
                else
                {
                    Console.WriteLine("\nTryk på en tast for at fortsætte.");
                }
                Console.ReadKey();
                Console.Clear();
            }

            // Vis den samlede score
            Console.WriteLine($"Quizzen er slut! Du fik {score} ud af {totalQuestions} rigtige.");
            Console.ReadKey();
        }



        // Vis den samlede score




        static void ShowMenu()
        {
            // Angiv stien til JSON-filen
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Vand_quiz.json");
            string jsonFilePath_Programmering = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "programmering_quiz.json");
            string jsonFilePath_SQL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL_quiz.json");

            // Indlæs og deserialiser JSON-filens indhold til et Quiz-objekt
            Quiz quiz_Vand = LoadQuizFromFile(jsonFilePath);
            Quiz quiz_Programmering = LoadQuizFromFile(jsonFilePath_Programmering);
            Quiz quiz_SQL = LoadQuizFromFile(jsonFilePath_SQL);


            // Vis menuen med quiz-valgmuligheder
            Console.WriteLine("Hvilken Quiz vil du starte?");
            Console.WriteLine("1. Vand Quiz");
            Console.WriteLine("2. Programmering Quiz");
            Console.WriteLine("3. SQL Quiz");

            // Læs brugerens valg
            Console.Write("Vælg en quiz (1-3): ");
            string choice = Console.ReadLine()?.Trim();

            Console.WriteLine();

            // Bestem hvilken quiz der skal startes
            switch (choice)
            {
                case "1":
                    RunQuiz(quiz_Vand);
                    break;
                case "2":
                    RunQuiz(quiz_Programmering);
                    break;
                case "3":
                    RunQuiz(quiz_SQL);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    ShowMenu(); // Genstart menuen ved ugyldigt valg
                    break;
            }
        }
    }

}

