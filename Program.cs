using System;
using System.Linq;
using System.Text;
using System.Text.Json;


namespace Quiz_Med_JSON_Serialisering
{

    internal class Program
    {





        static int correctindex_UserINPUT = 0;
        static int FindCorrectIndex;
        static int score = 0;
        static int totalQuestions;
        static string userInput;
        static string correctAnswer;
        static string GrabNumber;
        static string CorrectNumber = "test";

        static bool keepruning = true;
        static bool NumberSystem = false;
        static bool hasbenrigthbefore = false;
        static bool DontGrapNumber = false;

        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static Quiz LoadQuizFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(jsonString);
            return quiz;
        }

        private static void RunQuiz(Quiz quiz)
        {
            totalQuestions = quiz.Questions.Count;

            for (int currentQuestionIndex = 0; currentQuestionIndex < totalQuestions; currentQuestionIndex++)
            {
                var question = quiz.Questions[currentQuestionIndex];
                Console.WriteLine(question.Question);

                Increment_Question_Number(question);
                RUNNUMBERSYSTEM_HANDLE(question);
                CHECKINPUTVALUES(question);
                CheckIfLastQuestion(currentQuestionIndex);
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Hvilken Quiz vil du starte?");
            Console.WriteLine("1. Vand Quiz");
            Console.WriteLine("2. Programmering Quiz");
            Console.WriteLine("3. SQL Quiz");

            Console.Write("Vælg en quiz (1-3): ");
            LoadQuiz_UserPreference();
        }

        private static void LoadQuiz_UserPreference()
        {
            string jsonFilePath_Vand = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Vand_quiz.json");
            string jsonFilePath_Programmering = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "programmering_quiz.json");
            string jsonFilePath_SQL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL_quiz.json");

            Quiz quiz_Vand = LoadQuizFromFile(jsonFilePath_Vand);
            Quiz quiz_Programmering = LoadQuizFromFile(jsonFilePath_Programmering);
            Quiz quiz_SQL = LoadQuizFromFile(jsonFilePath_SQL);

            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    StartQuiz(quiz_Vand);
                    break;
                case "2":
                    StartQuiz(quiz_Programmering);
                    break;
                case "3":
                    StartQuiz(quiz_SQL);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    ShowMenu();
                    break;
            }
        }

        private static void StartQuiz(Quiz quiz)
        {
            ClearConsole();
            RunQuiz(quiz);
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private static void Increment_Question_Number(QuizQuestion question)
        {
            int index = 1;
            foreach (var option in question.AnswerOptions)
            {
                Console.WriteLine($"{index} * {option}");
                index++;
            }
        }

        private static void RUNNUMBERSYSTEM_HANDLE(QuizQuestion question)
        {
            keepruning = true;

            while (keepruning)
            {
                Console.Write("Indtast dit svar: ");
                userInput = Console.ReadLine()?.Trim();

                if (int.TryParse(userInput, out correctindex_UserINPUT))
                {
                    correctindex_UserINPUT--; // Convert 1-based index to 0-based
                    if (correctindex_UserINPUT >= 0 && correctindex_UserINPUT < question.AnswerOptions.Count)
                    {
                        correctAnswer = question.AnswerOptions[question.CorrectAnswer];
                        FindCorrectIndex = question.CorrectAnswer;
                        keepruning = false;
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    }
                }
                else if (question.AnswerOptions.Any(option => option.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    correctAnswer = question.AnswerOptions[question.CorrectAnswer];
                    FindCorrectIndex = question.AnswerOptions.IndexOf(correctAnswer);
                    correctindex_UserINPUT = question.AnswerOptions.IndexOf(userInput);
                    keepruning = false;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                }
            }
        }

        private static void CHECKINPUTVALUES(QuizQuestion question)
        {
            if (correctindex_UserINPUT == FindCorrectIndex || userInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                DisplayCorrectAnswerMessage(question);
            }
            else
            {
                DisplayWrongAnswer(question);
            }
        }

        private static void DisplayCorrectAnswerMessage(QuizQuestion question)
        {
            Console.WriteLine("\nKorrekt!\n");
            if (question.CorrectAnswerMessage != null && question.CorrectAnswerMessage.Count > 0)
            {
                foreach (var message in question.CorrectAnswerMessage)
                {
                    Console.WriteLine(message);
                }
            }
            score++;
        }

        private static void DisplayWrongAnswer(QuizQuestion question)
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

        private static void CheckIfLastQuestion(int currentQuestionIndex)
        {
            if (currentQuestionIndex == totalQuestions - 1)
            {
                Console.WriteLine($"Quizzen er slut! Du fik {score} ud af {totalQuestions} rigtige.");
                RestartProgram();
            }
            else
            {
                Console.WriteLine("\nTryk på en tast for at fortsætte.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void RestartProgram()
        {
            Console.WriteLine("Pres Q to Quit Program");
            Console.Write("Pres R to Restart Program");

            bool validInput = false;
            while (!validInput)
            {
                var info = Console.ReadKey();
                switch (info.KeyChar)
                {
                    case 'R':
                    case 'r':
                        ShowMenu();
                        validInput = true;
                        break;
                    case 'Q':
                    case 'q':
                        return;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }
    }
}






















