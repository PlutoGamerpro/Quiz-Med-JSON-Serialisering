# Quiz Med JSON Serialisering

🎉 Velkommen til Quiz Med JSON Serialisering! Dette program giver brugerne mulighed for at deltage i forskellige quizzer baseret på JSON-data❓💡.


### Explanation

- **`Solution/bin/Debug/netX.X/Debug/net7.0/data/`**: This is where your JSON data files are stored.
- **`Models/`**: Contains your model classes and files.
- **`Program/`**: Contains your main program files.
- **`Solution/`**: Contains your solution and project files.
- **`README.md`**: This file.

## 📜 Indhold

1. 📖 Introduktion
2. 🚀 Sådan kører du programmet
3. 🧩 Metoder
   - 🔄 Genstart Programmet
   - 📂 LoadQuizFromFile
   - ❓ RunQuiz
   - 🗒️ ShowMenu

## 📝 Introduktion

Dette program er designet til at hente quizspørgsmål fra JSON-filer og give brugerne mulighed for at besvare dem. Programmet vil evaluere svarene og give feedback.

## 🚀 Sådan kører du programmet

1. Sørg for, at du har .NET SDK installeret.
2. Klon repository'et til din lokale maskine.
3. Naviger til projektmappen i din terminal.
4. Kør programmet med følgende kommando:

  ```bash
   dotnet run
```

### 🧩 Metoder
## 🔄 Genstart Programmet
- Denne metode giver brugeren mulighed for at vælge at genstarte quizzen eller afslutte programmet.
Programmet viser en besked, hvor brugeren kan trykke på R for at genstarte eller Q for at afslutte.
Indtastningen valideres, og hvis inputtet ikke er gyldigt, vises en fejlmeddelelse, og brugeren bliver bedt om at prøve igen.
```csharp
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
```
## 📂 LoadQuizFromFile
- Henter quizdata fra en JSON-fil og deserialiserer den til et Quiz-objekt.
```csharp
private static Quiz LoadQuizFromFile(string filePath)
{
    string jsonString = File.ReadAllText(filePath);
    Quiz quiz = JsonSerializer.Deserialize<Quiz>(jsonString);
    return quiz;
}
```
## 🏃‍♂️ RunQuiz
- Kører quizzen og viser spørgsmålene til brugeren.
```csharp
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
```
## 📋 ShowMenu
- Viser menuen for brugeren, hvor de kan vælge hvilken quiz de vil deltage i.
```csharp

private static void ShowMenu()
{
    Console.WriteLine("Hvilken Quiz vil du starte?");
    Console.WriteLine("1. Vand Quiz");
    Console.WriteLine("2. Programmering Quiz");
    Console.WriteLine("3. SQL Quiz");

    Console.Write("Vælg en quiz (1-3): ");
    LoadQuiz_UserPreference();
}



```
## 🎲 JSON Structure & Quiz Question Generation 
This project uses JSON files to store quiz questions and answers. Each quiz consists of multiple-choice questions, and here's a breakdown of the structure:

### JSON Format 📄

The JSON file has the following format:

```json
{
  "Questions": [
    {
      "Question": "What is the correct SQL statement to retrieve data from a database?",
      "AnswerOptions": [
        "SELECT",
        "GET",
        "FETCH",
        "EXTRACT"
      ],
      "CorrectAnswer": 0,
      "CorrectAnswerMessage": [
        "The correct SQL statement to retrieve data is 'SELECT'.",
        "'SELECT' specifies which columns to return from a table."
      ]
    }
  ]
}
```
## Key Elements 🔑
- ❓ Questions: An array of quiz questions.
- 📝 Question: The text of the quiz question.
- ✔️ AnswerOptions: An array of possible answers.
- ✅ CorrectAnswer: An integer index indicating the correct answer from the AnswerOptions array (0-based index).
- 📚 CorrectAnswerMessage: An optional array of messages providing additional information about the correct answer.

## How Questions are Generated 🛠️
- 📂 Loading the Quiz: The quiz is loaded from the specified JSON file using System.Text.Json.
- 👀 Displaying Questions: Each question is displayed to the user, along with its answer options.
- ✍️ User Input: The user can input their answer by either entering the option number or the text of the answer.
- 🔍 Validation: The input is validated to ensure it matches one of the options.
- 🎉 Feedback: After the user answers, feedback is provided, including whether the answer was correct and additional information from the CorrectAnswerMessage.

## Example Question Flow 🏃‍♂️
- 📊 User sees the question: "What is the correct SQL statement to retrieve data from a database?"
### Answer Options
1. 📄 SELECT
2. 🔍 GET
3. 📦 FETCH
4. 🗑️ EXTRACT


 - User inputs 1 (or types "SELECT"). 🆗
 - The program checks if it's correct, provides feedback, and moves to the next question. 🔄

## 🙌 Tak for din tid!

Jeg håber, du fandt denne gennemgang nyttig! Hvis du er interesseret i at se flere af mine projekter, kan du tjekke dem ud [here](https://github.com/PlutoGamerpro?tab=stars).

