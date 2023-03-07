using System.Text.RegularExpressions;
using comroid.common;
using comroid.csapi.common;

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

    public static void Main(string[] args) => new Program(Difficulty.Easy, 4).play();

    public string[] Words { get; }
    public int UseIndex { get; }

    private Program(Difficulty difficulty, int letters, int wordCount = 8)
    {
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
            Console.WriteLine("Words available:");
            for (var i = 0; i < Words.Length; i++)
            {
                var word = Words[i];
                Console.WriteLine($"\t- {i}:\t{word}");
            }
        }
        
        PrintWords();

        while (!hacked && attemptsLeft > 0)
        {

            Console.Write("> ");
            var input = Console.ReadLine()!;
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
                Console.WriteLine($"Invalid [0x{CompareWords(solution, used):X}]; remaining {--attemptsLeft}");
            } else Console.WriteLine("You're in!");
        }
        
        if (!hacked)
            Console.WriteLine("Locked out");
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