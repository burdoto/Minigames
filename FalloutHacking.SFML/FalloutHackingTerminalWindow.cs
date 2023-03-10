using System.Numerics;
using comroid.gamelib;
using SFML.Graphics;

namespace FalloutHacking.SFML;

public class FalloutHackingTerminalWindow : GameBase
{
    public static Font RubikIso = new("Assets/RubikIso-Regular.ttf");
    public static FalloutHackingTerminalWindow Instance { get; private set; } = null!;

    public FalloutHackingTerminalWindow()
    {
        Background = new Color(0x313338ff);
        Add(new TerminalScreen(this));
    }

    public static void Main(string[] args)
    {
        (Instance = new FalloutHackingTerminalWindow()).Run();
    }
}

internal class TerminalScreen : Rect
{
    public TerminalScreen(IGameObject gameObject) : base(gameObject, gameObject)
    {
        Position = Vector3.One * 300;
        Scale = Vector3.One * 200;
        Delegate.FillColor = new Color(0xd7c4abff);

        Add(new Circle(GameObject) { Position = Vector3.One * 50, Radius = 60 });
    }
}
