using Microsoft.Xna.Framework;
using Platformer2D.Core;
using Platformer2D.Physics;
using Platformer2D.Rendering;

namespace Platformer2D.Entities;

public class Platform : Entity {
    
    public Platform() {

        AddComponent(new SpriteRenderer() {
            Size = new Vector2(1800, 40),
            Color = Color.Red
        });

        AddComponent(new BoxCollider() {
            Size = new Vector2(1800, 40),
            Layer = CollisionLayer.World,
            Mask = CollisionLayer.Player
        });
    }
}
