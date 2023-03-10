using SFML.Graphics;
using SFML.Window;

namespace comroid.ESim;

public class ESim
{
    public static void Main(string[] args)
    {
        var win = new RenderWindow(new VideoMode(600, 400), "ESim");
        win.SetVerticalSyncEnabled(true);
        win.SetFramerateLimit(60);

        while (win.IsOpen)
        {
            win.DispatchEvents();
            win.Clear(Color.Black);

            win.Draw(new CircleShape(10));
            
            win.Display();
        }
    }
}