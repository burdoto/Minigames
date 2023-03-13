using comroid.gamelib;

namespace Brick.NET;

public class Player : GameObject
{
    public Player()
    {
        // the bar
        Add(new Rect(this));
    }

    public void SpawnBall()
    {
    }
}