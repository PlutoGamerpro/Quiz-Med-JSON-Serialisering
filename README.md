# Quiz Med JSON Serialisering

🎉 Velkommen til Quiz Med JSON Serialisering! Dette program giver brugerne mulighed for at deltage i forskellige quizzer baseret på JSON-data.

## 📜 Indhold

1. [📖 Introduktion](#introduktion)
2. [🚀 Sådan kører du programmet](#sådan-kører-du-programmet)
3. [🧩 Metoder](#metoder)
   - [🔄 Genstart Programmet](#-genstart-programmet)
   - [📂 LoadQuizFromFile](LoadQuizFromFile)
   - [❓ RunQuiz](#-runquiz)
   - [🗒️ ShowMenu](#-showmenu)

## 📝 Introduktion

Dette program er designet til at hente quizspørgsmål fra JSON-filer og give brugerne mulighed for at besvare dem. Programmet vil evaluere svarene og give feedback.

## 🚀 Sådan kører du programmet

1. Sørg for, at du har .NET SDK installeret.
2. Klon repository'et til din lokale maskine.
3. Naviger til projektmappen i din terminal.
4. Kør programmet med følgende kommando:

 🛠️ Metoder
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


