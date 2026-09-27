using Microsoft.Xna.Framework;
using Platformer2D.Components;
using Platformer2D.Controllers;
using Platformer2D.Core;
using Platformer2D.Physics;
using Platformer2D.Rendering;

namespace Platformer2D.Entities;

public class Player : Entity {
    
    public Player() {
        AddComponent(new SpriteRenderer() {
            Size = new Vector2(50, 50),
            Color = Color.White
        });

        AddComponent(new BoxCollider() {
            Size = new Vector2(50, 50),
            Layer = CollisionLayer.Player,
            Mask = CollisionLayer.World | CollisionLayer.Trigger
        });
        
        AddComponent(new InputController());
        AddComponent(new PhysicsBody());
        AddComponent(new Health(200));
    }
}