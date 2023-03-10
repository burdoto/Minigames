using System.Numerics;
using comroid.common;
using comroid.gamelib;
using comroid.gamelib.Capability;
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
    private readonly Hoverable hoverable;
    private readonly Clickable clickable;

    public TerminalScreen(IGameObject gameObject) : base(gameObject, gameObject)
    {
        Position = Vector3.One * 300;
        Scale = Vector3.One * 200;

        Add(this.hoverable = new Hoverable(this));
        Add(this.clickable = new Clickable(this));
        clickable.Click += _ => Log.Debug.At(LogLevel.Info, "test");
    }

    public override bool Update()
    {
        if (clickable.Clicking)
            Delegate.FillColor = Color.Blue;
        else if (hoverable.Hovering)
            Delegate.FillColor = Color.Red;
        else Delegate.FillColor = new Color(0xd7c4abff);
        return base.Update();
    }
}
