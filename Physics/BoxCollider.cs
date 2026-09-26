using Microsoft.Xna.Framework;
using Platformer2D.Core;

namespace Platformer2D.Physics;

public class BoxCollider : Component {
    public Vector2 Size { get; set; }

    public Bounds Bounds {
        get {
            var position = Entity!.Transform.Position;
            return new Bounds(
                position.X,
                position.Y,
                Size.X,
                Size.Y
            );
        }
    }
}