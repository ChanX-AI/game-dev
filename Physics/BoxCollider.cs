using Microsoft.Xna.Framework;
using Platformer2D.Core;

namespace Platformer2D.Physics;

public class BoxCollider : Component {
    public Vector2 Size { get; set; }
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