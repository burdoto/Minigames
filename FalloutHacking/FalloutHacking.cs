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
    // the starting point of the application
    public static void Main(string[] args) =>
        // create a new game instance
        new FalloutHacking(new ConsoleUIModule(), Difficulty.Easy, new Random().Next(4, 10))
            // and start it
            .play();

    // somewhere to store our difficulty
    private readonly Difficulty _difficulty;
    // somewhere to store our user interface
    public UIModule UI { get; }
    // somewhere to store all words available in this game round
    public string[] Words { get; }
    // somewhere to store the index of the solution word
    public int UseIndex { get; }

    // game constructor
    private FalloutHacking(UIModule ui, Difficulty difficulty, int letters, int wordCount = -1)
    {
        // store variables
        this.UI = ui;
        this._difficulty = difficulty;
        this.UseIndex = new Random().Next(0, wordCount);
        // if wordCount was not set, set it to its default value
        if (wordCount == -1)
            wordCount = 32 / (int)difficulty;
        // fetch all words with the specified letter count
        var dict = ((IDictionary<string, JsonNode?>)JsonNode.Parse(new HttpClient()
                    .GetStringAsync(
                        new Uri("https://raw.githubusercontent.com/dwyl/english-words/master/words_dictionary.json"))
                    .Await())!
                .AsObject()).Keys
            .Where(w => w.Length == letters)
            .ToArray();
        // create a shuffled list of possible words
        do this.Words = dict.Shuffle().ToArray()[..wordCount];
        // until the list contains a feasible amount of possible words regarding the difficulty
        // so that you wont be having a couple of COMPLETELY different words
        while (Words.Count(w => CompareWords(Words[UseIndex], w) == 0) > wordCount * ((int)difficulty / wordCount));
    }

    private void play()
    {
        // variables
        var attemptsLeft = (int)_difficulty;
        var hacked = false;

        // list all possible words for this round
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

        // game loop
        // as long as the game was not beaten and there are any attempts left, do the following:
        while (!hacked && attemptsLeft > 0)
        {
            // read a line from the user interface (in this case: from console)
            var input = UI.WaitForInput()!;
            // store the solution for ease
            var solution = Words[UseIndex];
            
            string used;
            // if the input is a number
            if (Regex.IsMatch(input, "\\d+"))
                // parse it, and use it as an index for all possible words to get the desired input word
                used = Words[int.Parse(input)];
            // otherwise, if the input equals the solution (not case sensitive)
            else if (solution.Equals(input, StringComparison.InvariantCultureIgnoreCase))
                // set the solution so that the case is preserved
                used = solution;
            // otherwise just store the input
            else used = input;
            
            // if the input equals the solution, the game was beaten
            hacked = used == solution;

            // if the game was not beaten, ..
            if (!hacked)
            {
                // write that it was not beaten
                UI.WriteOutput($"Invalid password [{CompareWords(solution, used)}]; remaining {--attemptsLeft}");
            } else 
                // otherwise print that it was beaten
                UI.WriteOutput("You're in!");
        }
        
        // if the game was not beaten in the end, inform the user
        if (!hacked)
            UI.WriteOutput($"Locked out; password was {Words[UseIndex]}");
    }

    // get the amount of matching characters of two words
    private static int CompareWords(string solution, string used)
    {
        // a counter for the matching characters
        var match = 0;
        
        // go through all characters
        for (
            // from 0
            var i = 0;
            // to the end of the shorter one out of [solution, used]
            i < Math.Min(solution.Length, used.Length);
            // advance by 1 after every check
            i++)
            // if the characters at position i inside solution and used are equal, then
            if (solution[i] == used[i])
                // increment the matching character counter by 1
                match++;
        // and return the matching character counter
        return match;
    }
}