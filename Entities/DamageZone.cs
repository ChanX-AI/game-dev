using Microsoft.Xna.Framework;
using Platformer2D.Components;
using Platformer2D.Core;
using Platformer2D.Physics;
using Platformer2D.Rendering;

namespace Platformer2D.Entities;

public class DamageZone : Entity {

    public DamageZone() {
        AddComponent(new SpriteRenderer() {
            Size = new Vector2(100, 40),
            Color = Color.BurlyWood
        });

        AddComponent(new BoxCollider() {
            Size = new Vector2(100, 40),
            IsTrigger = true,
            Layer = CollisionLayer.Trigger,
            Mask = CollisionLayer.Player
        });

        AddComponent(new DamageZoneComponent(10));
    }
}