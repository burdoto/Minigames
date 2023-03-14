using System.Numerics;
using comroid.common;
using comroid.gamelib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Brick.NET;

public class Player : GameObject
{
    private readonly int number = 0;
    private readonly Rect.Collider area;
    private readonly Rect bar;
    internal PlayBall? attached;

    public Player(GameBase game) : base(game)
    {
        this.Name = "Player" + (number + 1);
        // player area
        var area = BrickNET.PlayerArea[number];
        var rect = new Rect(this)
        {
            Position = area.position.To3(-1),
            Size = area.size.To2f(),
            Color = new Color(0x222222ff)
        };
        // area marker
        this.area = new Rect.Collider(this, rect);
#if DEBUG
        Add(rect);
#endif

        // the bar
        this.bar = new Rect(this) { Position = area.position.To3(), Size = new Vector2f(250, 20) };
        bar.Add<Rect.Collider>();
        Add(bar);
    }

    public override bool Update()
    {
        var pos = Input.MousePosition;
        var isPointInside = area.IsPointInside(pos);
        if (isPointInside)
            bar.Position = pos.To3();

        if (Input.GetKey(Mouse.Button.Left) && attached != null)
        {
            attached.ReleaseFromBar();
            attached = null;
        }
        else if (attached != null) attached.Position = bar.Position - Vector3.UnitY * 26;

        return base.EarlyUpdate();
    }

    public void SpawnBall()
    {
        if (attached == null)
            Game.Add(attached = new PlayBall(Game));
    }
}