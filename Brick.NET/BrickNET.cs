using System.Numerics;
using comroid.gamelib;
using SFML.Graphics;
using Text = comroid.gamelib.Text;

namespace Brick.NET;

public class BrickNET : GameBase
{
    public static BrickNET Instance { get; private set; } = null!;

    public long Score
    {
        get => long.Parse(score.Value);
        set => score.Value = value.ToString();
    }
    private Text score;

    public BrickNET(RenderWindow window = null!) : base(window)
    {
        Instance = this;

        // order sensitive!
        Add(new Board(this));
        Add(new Player(this));
        Add(score = new Text(this) { Value = "0", Position = Vector3.UnitY * 100 });
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
