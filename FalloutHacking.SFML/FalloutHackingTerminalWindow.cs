using System.Numerics;
using comroid.common;
using comroid.gamelib;
using comroid.gamelib.Capability;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Text = comroid.gamelib.Text;

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
        if (Input.GetKey(Keyboard.Key.W)) Camera.Position += new Vector3(0, -1, 0);
        if (Input.GetKey(Keyboard.Key.S)) Camera.Position += new Vector3(0, 1, 0);
        if (Input.GetKey(Keyboard.Key.A)) Camera.Position += new Vector3(-1, 0, 0);
        if (Input.GetKey(Keyboard.Key.D)) Camera.Position += new Vector3(1, 0, 0);
#endif

        return base.Update();
    }

    public static void Main(string[] args) => new FalloutHackingTerminalWindow().Run();
}

internal class TerminalScreen : Rect
{
    public TerminalScreen(IGameObject gameObject) : base(gameObject, gameObject)
    {
        Size = new Vector2f(1200,800);
        Color = new Color(0x9a8c7bff);

        var bg = new Rect(GameObject, this) { Size = new Vector2f(1000, 750), Color = Color.Black };
        Add(bg);
        Add(new TitleBox(GameObject, bg){Position = Vector3.UnitY * -250, Size = new Vector2f(950, 200) });
        Add(new MemBox(GameObject, bg){Position = new Vector3(-320, 90, 0), Size = new Vector2f(340, 250) });
        Add(new MemBox(GameObject, bg){Position = new Vector3(40, 90, 0), Size = new Vector2f(340, 250) });
        Add(new ConsoleBox(GameObject, bg){Position = new Vector3(365, 90, 0), Size = new Vector2f(250, 200) });
    }
    
    private class TitleBox : Rect
    {
        public TitleBox(IGameObject gameObject, ITransform transform = null!) : base(gameObject, transform)
        {
            Add(new Text(GameObject, this) { Value = "Enter Password" });
        }
    }
    
    private class MemBox : Rect
    {
        public MemBox(IGameObject gameObject, ITransform transform = null!) : base(gameObject, transform)
        {
        }
    }
    
    private class ConsoleBox : Rect
    {
        public ConsoleBox(IGameObject gameObject, ITransform transform = null!) : base(gameObject, transform)
        {
        }
    }
}
