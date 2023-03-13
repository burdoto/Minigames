using System.Numerics;
using comroid.gamelib;
using comroid.gamelib.Capability;

namespace Brick.NET;

public class PlayBall : GameObject
{
    private readonly Rigidbody rigidbody;

    public PlayBall(GameBase game) : base(game)
    {
        Add(new Circle(this) { Radius = 15 });
        this.rigidbody = Add<Rigidbody>()!;
        rigidbody.Collide += OnCollide;
    }

    private void OnCollide(ICollider obj)
    {
    }

    public void ReleaseFromBar()
    {
        rigidbody.Velocity = -Vector3.UnitY;
    }
}