using System.Numerics;
using comroid.common;
using comroid.gamelib;
using comroid.gamelib.Capability;
using SFML.Graphics;
using SFML.System;

namespace Brick.NET;

public class Brick : GameObject
{
    public const string NameId = "Brick";
    public const int Width = 75;
    public const int Height = 25;

    public int value => (int)FindComponent<Rect>()!.Color.ToInteger();

    public Brick(BrickNET game, Vector2 pos) : base(game)
    {
        Channel = Channel.Props;
        Position = pos.To3();
        var r = Add<Rect>()!;
        r.Size = new Vector2f(Width, Height);
        r.Color = new Color((uint)DebugUtil.RNG.Next(int.MaxValue) | 0x000000ff);
        r.Add<Rect.Collider>();
        Add<Rigidbody>()!.Bounciness = 1;
    }

    public new void Destroy()
    {
        Game.As<BrickNET>()!.Score += value;
        base.Destroy();
    }
}
