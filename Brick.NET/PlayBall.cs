﻿using System.Numerics;
using comroid.common;
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
        this.rigidbody = Add<Rigidbody>()!;
        rigidbody.Bounciness = 1;
        rigidbody.Collide += OnCollide;
    }

    private void OnCollide(Collision collision)
    {
        if (!(collision.Cancelled = collision.CollidedWith.GameObject?.Name != Brick.NameId &&
                                  (!collision.CollidedWith.GameObject?.Name.StartsWith("Player") ?? false)))
        {
            if (collision.CollidedWith.GameObject?.As<Brick?>() is { } brick)
            {
                Game.As<BrickNET>().Score += brick.value;
                brick.Destroy();
            }
        }
    }

    public void ReleaseFromBar()
    {
        rigidbody.Velocity = -Vector3.UnitY * 0.2f;
    }
}