using System;
using System.Text.Json;


namespace Quiz_Med_JSON_Serialisering
{

    internal class Program
    {

        static bool NumberSystem = false;
        static void Main(string[] args)
        {
            ShowMenu();
        }
        private static Quiz LoadQuizFromFile(string filePath)
        {
            // Læs JSON-filens indhold
            string jsonString = File.ReadAllText(filePath);

            // Deserialiser JSON-strengen til et Quiz-objekt
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(jsonString);

            return quiz;
        }

        private static void RunQuiz(Quiz quiz)
        {
            int totalQuestions = quiz.Questions.Count;
            int score = 0;

            for (int currentQuestionIndex = 0; currentQuestionIndex < totalQuestions; currentQuestionIndex++)
            {
                var question = quiz.Questions[currentQuestionIndex];
                Console.WriteLine(question.Question);

                char index = '1';
                int correctindex_UserINPUT = 0;
                int FindCorrectIndex = 0;
                string userInput = "";
                string correctAnswer = "";
                bool keepruning = true;


                Increment_Question_Number(index, question);
                RUNNUMBERSYSTEM_HANDLE(keepruning, correctindex_UserINPUT, correctAnswer, question);
                CHECKINPUTVALUES(userInput, correctAnswer, correctindex_UserINPUT, FindCorrectIndex, question, question.CorrectAnswerMessage, score);
                CheckIfLastQuestion(score, totalQuestions, currentQuestionIndex);


                Console.WriteLine(correctindex_UserINPUT +  " corretindex user");
                Console.WriteLine(FindCorrectIndex +  " find correct index ");
                Console.WriteLine(userInput + " userinput ")  ;
                Console.WriteLine(correctAnswer + "  correctanswer is ");
                            



            }
        }

        private static void ShowMenu()
        {
            // Vis menuen med quiz-valgmuligheder
            Console.WriteLine("Hvilken Quiz vil du starte?");
            Console.WriteLine("1. Vand Quiz");
            Console.WriteLine("2. Programmering Quiz");
            Console.WriteLine("3. SQL Quiz");

            // Læs brugerens valg
            Console.Write("Vælg en quiz (1-3): ");

            LoadQuiz_UserPrenfence();
        }
        private static void LoadQuiz_UserPrenfence()
        {
            // Angiv stien til JSON-filen
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Vand_quiz.json");
            string jsonFilePath_Programmering = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "programmering_quiz.json");
            string jsonFilePath_SQL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL_quiz.json");

            // Indlæs og deserialiser JSON-filens indhold til et Quiz-objekt
            Quiz quiz_Vand = LoadQuizFromFile(jsonFilePath);
            Quiz quiz_Programmering = LoadQuizFromFile(jsonFilePath_Programmering);
            Quiz quiz_SQL = LoadQuizFromFile(jsonFilePath_SQL);

            string choice = Console.ReadLine()?.Trim();

            Console.WriteLine();

            // Bestem hvilken quiz der skal startes
            LoadCases(choice, quiz_Vand, quiz_Programmering, quiz_SQL);
        }
        private static void LoadCases(string choice, Quiz quiz_Vand, Quiz quiz_Programmering, Quiz quiz_SQL)
        {
            // Bestem hvilken quiz der skal startes
            switch (choice)
            {
                case "1":
                    Type_1_OR_FullAnswer();
                    RunQuiz(quiz_Vand);
                    break;
                case "2":
                    Type_1_OR_FullAnswer();
                    RunQuiz(quiz_Programmering);
                    break;
                case "3":
                    Type_1_OR_FullAnswer();
                    RunQuiz(quiz_SQL);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    ShowMenu(); // Genstart menuen ved ugyldigt valg
                    break;
            }
        }

        private static void Increment_Question_Number(int index, QuizQuestion question)
        {
            index = 1;

            // Vis svarmulighederne med stjerner (*)
            foreach (var option in question.AnswerOptions)
            {
                Console.WriteLine($" {index} * {option}");
                index++;
            }

        }

        private static void Type_1_OR_FullAnswer()
        {
            bool keepruning = true;


            while (keepruning)
            {
                Console.WriteLine("Option du answer questions");
                Console.WriteLine("1 2 3 etc system TYPE NUMBER for this system");


                Console.WriteLine("Type the hole answeroption you think TYPE HOLE for this system");
                Console.WriteLine("");

                string userinput = Console.ReadLine().ToLower();


                if (userinput == "number") { NumberSystem = true; keepruning = false; }
                else if (userinput == "hole") { NumberSystem = false; keepruning = false; }
            }


        }
        private static void CheckIfLastQuestion(int score, int totalQuestions, int currentQuestionIndex)
        {
            if (currentQuestionIndex == totalQuestions - 1)
            {
                Console.WriteLine("\nQuizzen er slut! Tryk på en tast for at afslutte.");
            }
            else
            {



                Console.WriteLine("\nTryk på en tast for at fortsætte.");

                Console.ReadKey();
                Console.Clear();

                // Vis den samlede score
                Console.WriteLine($"Quizzen er slut! Du fik {score} ud af {totalQuestions} rigtige.");
                Console.ReadKey();
            }

        }




        private static void RUNNUMBERSYSTEM_HANDLE(bool keepruning, int correctindex_UserINPUT, string correctAnswer, QuizQuestion question)
        {
            if (NumberSystem)
            {
                while (keepruning)
                {
                    Console.Write("Indtast dit svar: ");
                    string userinput = Console.ReadLine()?.Trim();

                    if (int.TryParse(userinput, out correctindex_UserINPUT))
                    {

                        keepruning = false;

                        correctAnswer = question.AnswerOptions[question.CorrectAnswer];

                        Console.WriteLine(" answer " + correctAnswer);
                        Console.WriteLine("answer userinput index " + correctindex_UserINPUT);


                    }
                    else
                    {
                        Console.WriteLine("Cant handle other things than number");
                        keepruning = true;
                        Console.WriteLine("");
                    }

                }

            }
        }



        private static void CHECKINPUTVALUES(string userInput, string correctAnswer, int correctindex_UserINPUT, int FindCorrectIndex, QuizQuestion question, List<string> CorrectAnswerMessage, int score)
        {
            // Check om brugerens input er lig med det korrekte svar


            if (!NumberSystem)
            {

                if (userInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {

                    DisplayCorrectAnswerMessage(correctAnswer, question, score, CorrectAnswerMessage);
                }
                else { DisplayWrongAnswer(correctAnswer, question, score, CorrectAnswerMessage); }
            }
            else
            {
                if (correctindex_UserINPUT == FindCorrectIndex)
                {
                    DisplayCorrectAnswerMessage(correctAnswer, question, score, CorrectAnswerMessage);
                }
                else { DisplayWrongAnswer(correctAnswer, question, score, CorrectAnswerMessage); }
            }
        }


        private static void DisplayCorrectAnswerMessage(string correctAnswer, QuizQuestion question, int score, List<string> CorrectAnswerMessage)
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
            else
            {
                DisplayWrongAnswer(correctAnswer, question, score, CorrectAnswerMessage);
            }
            score++;
        }


        private static void DisplayWrongAnswer(string correctAnswer, QuizQuestion question, int score, List<string> CorrectAnswerMessage)
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



    }

}






