using System.Numerics;
using comroid.gamelib;
using comroid.gamelib.Capability;
using SFML.Graphics;
using SFML.System;
using Text = comroid.gamelib.Text;

namespace Brick.NET;

public class BrickNET : GameBase
{
    public const int Spacing = 5;
    public static readonly (Vector2 position, Vector2 size)[] PlayerArea = new[]
        { (new Vector2(0, 350), new Vector2(1700, 200)) };
    public static readonly Vector2 PlayArea = new(1700, 900);
    public static BrickNET Instance { get; private set; } = null!;
    private Text score;

    public long Score
    {
        get => long.Parse(score.Value);
        set => score.Value = value.ToString();
    }

    public BrickNET(RenderWindow window = null!) : base(window)
    {
        Instance = this;
        
        var r = Add<Rect>("PlayArea")!;
        r.Size = PlayArea.To2f();
        r.Color = new Color(0x111111ff);
        var c = r.Add<Rect.Collider>()!;
        c.Inverse = true; //todo Fix
        // order sensitive!
        FillBoard();
        Add(new Player(this));
        Add(score = new Text(this) { Value = "0", Position = Vector3.UnitY * 100 });
    }
    

    private void FillBoard()
    {
        for (var x = -8; x <= 8; x++)
        for (var y = -16; y <= 0; y++)
            Add(new Brick(this, new Vector2(x * Brick.Width + (x - 1) * Spacing, y * Brick.Height + (y - 1) * Spacing)));
    }

    public override bool Enable()
    { 
        FindComponent<Player>()!.SpawnBall();

        return base.Enable();
    }

    public static void Main(string[] args)
    {
        new BrickNET().Run();
    }
}
