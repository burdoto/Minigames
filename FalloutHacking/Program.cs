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

public class Program
{
    private readonly Difficulty _difficulty;

    public static readonly Dictionary<int, string[]> WordRepo = new()
    {
        {
            4,
            new[]
            {
                "Door", "Home", "Room", "Tree", "Pear", "Bone",
                "Dash", "Four", "Pure", "Poor", "Duck", "Fish",
                "Five", "Nine", "Army", "Baby", "Dark", "Farm"
            }
        }
    };

    public static void Main(string[] args) => new Program(new ConsoleUIModule(), Difficulty.Easy, 4).play();

    public UIModule UI { get; }
    public string[] Words { get; }
    public int UseIndex { get; }

    private Program(UIModule ui, Difficulty difficulty, int letters, int wordCount = 8)
    {
        this.UI = ui;
        this._difficulty = difficulty;
        this.UseIndex = new Random().Next(0, wordCount);
        do this.Words = WordRepo[letters].Shuffle().ToArray()[..wordCount];
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
                UI.WriteOutput($"Invalid [0x{CompareWords(solution, used):X}]; remaining {--attemptsLeft}");
            } else UI.WriteOutput("You're in!");
        }
        
        if (!hacked)
            UI.WriteOutput("Locked out");
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