using System.Numerics;
using comroid.common;
using comroid.gamelib;
using comroid.gamelib.Capability;

namespace Brick.NET;

public class PlayBall : GameObject
{
    public const int Radius = 15;
    public const float SpawnSpeed = 0.5f;
    private readonly Rigidbody rigidbody;

    public PlayBall(GameBase game) : base(game)
    {
        Channel = Channel.Props | Channel.Player;
        var c = Add<Circle>()!;
        c.Radius = Radius;
        c.Add<Circle.Collider>();
        this.rigidbody = Add<Rigidbody>()!;
        rigidbody.Bounciness = 1;
        rigidbody.Collide += OnCollide;
        rigidbody.PositionFreeze = Vector3.UnitZ;
    }

    private void OnCollide(Collision collision)
    {
        if (collision.CollidedWith.GameObject?.As<Brick?>() is { } brick) 
            brick.Destroy();
    }

    public void ReleaseFromBar()
    {
        rigidbody.Velocity = new Vector3(0.5f, -1, 0) * SpawnSpeed;
    }
}