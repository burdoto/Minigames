using System.Numerics;
using comroid.gamelib;

namespace Brick.NET;

public class Board : GameObject
{
    public (Vector2 position, Vector2 size)[] PlayerArea => new[] { (new Vector2(0, 350), new Vector2(1700, 200)) };

    public Board(GameBase game) : base(game)
    {
    }
}