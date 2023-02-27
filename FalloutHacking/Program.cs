using comroid.csapi.common;

namespace FalloutHacking;

public class Program
{
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

    public static void Main(string[] args) => new Program(4).play();

    public string[] Words { get; set; }

    private Program(int letters, int wordCount = 8)
    {
        this.Words = WordRepo[letters].Shuffle().ToArray()[..wordCount];
        this.UseIndex = new Random().Next(0, wordCount);
    }

    private void play()
    {
        var attempts = 3;
        var hacked = false;

        while (!hacked && attempts > 0)
        {
            var input = Console.ReadLine();
        }
    }
}