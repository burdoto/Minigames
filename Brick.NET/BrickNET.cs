using comroid.gamelib;
using SFML.Graphics;

namespace Brick.NET;

public class BrickNET : GameBase
{
    public static BrickNET Instance { get; private set; } = null!;

    public BrickNET(RenderWindow window = null!) : base(window)
    {
        Instance = this;

        // order sensitive!
        Add(new Board(this));
        Add(new Player(this));
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
