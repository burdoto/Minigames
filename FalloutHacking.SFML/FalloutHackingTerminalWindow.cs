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
        Instance = this;
        Background = new Color(0x313338ff);
        Add(new TerminalScreen(this));
    }

    public override bool Update()
    {
#if DEBUG
        if (Input.GetKey(Keyboard.Key.W)) Camera.Position += new Vector3(0, -3, 0);
        if (Input.GetKey(Keyboard.Key.S)) Camera.Position += new Vector3(0, 3, 0);
        if (Input.GetKey(Keyboard.Key.A)) Camera.Position += new Vector3(-3, 0, 0);
        if (Input.GetKey(Keyboard.Key.D)) Camera.Position += new Vector3(3, 0, 0);
#endif

        return base.Update();
    }

    public static void Main(string[] args) => new FalloutHackingTerminalWindow().Run();
}

internal class TerminalScreen : Rect
{
    public TerminalScreen(IGameObject gameObject) : base(gameObject, gameObject)
    {
        Position = FalloutHackingTerminalWindow.Instance.Window.GetView().Center.To3();
        Scale = new Vector3(1200,800,0);
        Color = new Color(0x9a8c7bff);

        Add(new Rect(GameObject, this) { Scale = Vector3.One * 0.85f, Color = Color.Black });
    }
}
