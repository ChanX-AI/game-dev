using Microsoft.Xna.Framework;
using Platformer2D.Core;

namespace Platformer2D.Physics;

public class BoxCollider : Component {
    public Vector2 Size { get; set; }

    public float Left => Entity!.Transform.Position.X;
    public float Right => Entity!.Transform.Position.X + Size.X;
    public float Top => Entity!.Transform.Position.Y;
    public float Bottom => Entity!.Transform.Position.Y + Size.Y;
    public Vector2 Center => new(
        (Left + Right) / 2,
        (Top + Bottom) / 2
    );

    public Rectangle Bounds {
        get {
            var position = Entity!.Transform.Position;
            return new Rectangle(
                (int) position.X,
                (int) position.Y,
                (int) Size.X,
                (int) Size.Y
            );
        }
    }
}