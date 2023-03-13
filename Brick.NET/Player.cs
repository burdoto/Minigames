using System.Numerics;
using comroid.common;
using comroid.gamelib;
using SFML.Graphics;
using SFML.System;

namespace Brick.NET;

public class Player : GameObject
{
    private readonly int number = 0;
    private readonly Rect.Collider area;
    private readonly Rect bar;
    private PlayBall? attached;

    public Player(GameBase game) : base(game)
    {
        // player area
        var area = game.FindComponent<Board>()!.PlayerArea[number];
        var rect = new Rect(this)
        {
            Position = area.position.To3(),
            Size = area.size.To2f(),
            Color = new Color(0x222222ff)
        };
        // area marker
        this.area = new Rect.Collider(this, rect);
#if DEBUG
        Add(rect);
#endif

        // the bar
        this.bar = new Rect(this) { Size = new Vector2f(250, 20) };
        Add(bar);
    }

    public override bool Update()
    {
        var pos = Input.MousePosition;
        var isPointInside = area.IsPointInside(pos);
        if (isPointInside)
            bar.Position = pos.To3();
        return base.EarlyUpdate();
    }

    public void SpawnBall()
    {
        if (attached == null)
            Game.Add(attached = new PlayBall(Game));
    }
}