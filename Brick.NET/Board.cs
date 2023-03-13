using System.Numerics;
using comroid.gamelib;
using SFML.System;

namespace Brick.NET;

public class Board : GameObject
{
    public const int BoxWidth = 75;
    public const int BoxHeight = 25;
    public const int Spacing = 5;
    public (Vector2 position, Vector2 size)[] PlayerArea => new[] { (new Vector2(0, 350), new Vector2(1700, 200)) };

    public Board(GameBase game) : base(game)
    {
        var size = new Vector2f(BoxWidth, BoxHeight);
        for (var x = -8; x <= 8; x++)
        for (var y = -16; y <= 0; y++)
        {
            void CreateBox(Vector2 pos)
            {
                var box = new Rect(this) { Position = pos.To3(), Size = size };
                box.Add(new Rect.Collider(this, box));
                Add(box);
            }

            CreateBox(new Vector2(x * BoxWidth + (x - 1) * Spacing, y * BoxHeight + (y - 1) * Spacing));
        }
    }
}