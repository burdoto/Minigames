using System.Numerics;
using comroid.gamelib;
using comroid.gamelib.Capability;

namespace Brick.NET;

public class PlayBall : GameObject
{
    public const int Radius = 15;
    private readonly Rigidbody rigidbody;

    public PlayBall(GameBase game) : base(game)
    {
        var c = Add<Circle>()!;
        c.Radius = Radius;
        c.Add<Circle.Collider>();
        this.rigidbody = Add<Rigidbody>("PlayBall")!;
        rigidbody.Bounciness = 1;
        rigidbody.Collide += OnCollide;
    }

    private void OnCollide(ICollider obj)
    {
        Console.WriteLine($"{this} collided with {obj.GameObject}");
    }

    public void ReleaseFromBar()
    {
        rigidbody.Velocity = -Vector3.UnitY * 0.1f;
    }
}