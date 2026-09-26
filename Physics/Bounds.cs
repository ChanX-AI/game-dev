using Microsoft.Xna.Framework;

namespace Platformer2D.Physics;

public struct Bounds {
    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

    public Vector2 Center => new(
        (Left + Right) / 2,
        (Top + Bottom) / 2
    );

    public Bounds(float left, float top, float width, float height) {
        Left = left;
        Right = left + width;
        Top = top;
        Bottom = top + height;
    }

    public bool Intersects(Bounds other) {
        return (
            Left < other.Right &&
            Right > other.Left &&
            Top < other.Bottom &&
            Bottom > other.Top
        );

    }
}