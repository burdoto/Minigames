using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using comroid.common;
using comroid.textUI;
using comroid.textUI.stdio;

namespace FalloutHacking;

public enum Difficulty : int
{
    Peaceful = int.MaxValue,
    Easy = 4,
    Normal = 3,
    Hard = 2
}

public class FalloutHacking
{
    private readonly Difficulty _difficulty;

    public static void Main(string[] args) => new FalloutHacking(new ConsoleUIModule(), Difficulty.Easy, new Random().Next(4, 10)).play();

    public UIModule UI { get; }
    public string[] Words { get; }
    public int UseIndex { get; }

    private FalloutHacking(UIModule ui, Difficulty difficulty, int letters, int wordCount = -1)
    {
        this.UI = ui;
        this._difficulty = difficulty;
        if (wordCount == -1)
            wordCount = 32 / (int)difficulty;
        this.UseIndex = new Random().Next(0, wordCount);
        var dict = ((IDictionary<string, JsonNode?>)JsonNode.Parse(new HttpClient()
                    .GetStringAsync(
                        new Uri("https://raw.githubusercontent.com/dwyl/english-words/master/words_dictionary.json"))
                    .Await())!
                .AsObject()).Keys
            .Where(w => w.Length == letters)
            .ToArray();
        do this.Words = dict.Shuffle().ToArray()[..wordCount];
        while (Words.Count(w => CompareWords(Words[UseIndex], w) == 0) > wordCount * ((int)difficulty / wordCount));
    }

    private void play()
    {
        var attemptsLeft = (int)_difficulty;
        var hacked = false;

        void PrintWords()
        {
            UI.WriteOutput("Words available:");
            for (var i = 0; i < Words.Length; i++)
            {
                var word = Words[i];
                UI.WriteOutput($"\t- {i}:\t{word}");
            }
        }
        
        PrintWords();

        while (!hacked && attemptsLeft > 0)
        {
            var input = UI.WaitForInput()!;
            var solution = Words[UseIndex];
            string used;
            if (Regex.IsMatch(input, "\\d+"))
                used = Words[int.Parse(input)];
            else if (solution.Equals(input, StringComparison.InvariantCultureIgnoreCase))
                used = solution;
            else used = input;
            
            hacked = used == solution;

            if (!hacked)
            {
                UI.WriteOutput($"Invalid password [0x{CompareWords(solution, used):X}]; remaining {--attemptsLeft}");
            } else UI.WriteOutput("You're in!");
        }
        
        if (!hacked)
            UI.WriteOutput($"Locked out; password was {Words[UseIndex]}");
    }

    private static int CompareWords(string solution, string used)
    {
        var match = 0;
        for (var i = 0; i < Math.Min(solution.Length, used.Length); i++)
            if (solution[i] == used[i])
                match++;
        return match;
    }
}