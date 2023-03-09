using System.Security.Principal;
using SFML.Window;

namespace comroid.ESim;

public class ESim
{
    public static void Main(string[] args)
    {
        var win = new Window(new VideoMode(600, 400), "ESim");
        win.SetVerticalSyncEnabled(true);
        win.SetFramerateLimit(60);

        while (win.IsOpen)
        {
            win.

            win.DispatchEvents();
        }
    }
}