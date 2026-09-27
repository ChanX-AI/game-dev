using Microsoft.Xna.Framework;
using Platformer2D.Controllers;
using Platformer2D.Entities;
using Platformer2D.Physics;
using Platformer2D.Rendering;

namespace Platformer2D.Scenes;

public class Level1 : Scene {

    public void Load() {
        Player player = new();
        Platform platform = new();
        Platform divider = new();

        platform.Transform.Position = new Vector2(0, 400);
        divider.Transform.Position = new Vector2(200, 200);

        platform.AddComponent(new BoxCollider() {
            Size = new Vector2(1400, 50),
            Layer = CollisionLayer.World,
            Mask = CollisionLayer.Player
        });

        divider.AddComponent(new BoxCollider() {
            Size = new Vector2(100, 200),
            Layer = CollisionLayer.World,
            Mask = CollisionLayer.Player,
            IsTrigger = true
        });

        platform.AddComponent(new SpriteRenderer() {
            Size = new Vector2(1400, 50),
            Color = Color.White
        });

        divider.AddComponent(new SpriteRenderer() {
            Size = new Vector2(100, 200),
            Color = Color.White
        });
        
        player.AddComponent(new SpriteRenderer() {
            Size = new Vector2(50, 70),
            Color = Color.Blue
        });

        player.AddComponent(new BoxCollider() {
            Size = new Vector2(50, 70),
            Layer = CollisionLayer.Player,
            Mask = CollisionLayer.World | CollisionLayer.Enemy
        });
        
        player.AddComponent(new PhysicsBody());
        player.AddComponent(new InputController());
        player.AddComponent(new TestTrigger());

        Add(player);
        Add(platform);
        Add(divider);
    }
}