using comroid.gamelib;
using SFML.Graphics;

namespace Brick.NET;

public class PlayBall : GameObject
{
    public PlayBall(GameBase game) : base(game)
    {
        Add(new Circle(this) { Radius = 15, Color = Color.Blue });
    }

    public void Release()
    {
        throw new NotImplementedException();
    }
}