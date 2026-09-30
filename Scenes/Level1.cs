using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;
using Platformer2D.Entities;

namespace Platformer2D.Scenes;

public class Level1 : Scene {
    public Player Player { get; private set; } = null!;
    public override Entity? CameraTarget => Player;
    
    public override void Load(ContentManager content) {
        Texture2D mapTexture = content.Load<Texture2D>("full_map");
        Player = new Player();
        var platfom = new Platform();
        var damageZone = new DamageZone();
        var worldMap = new WorldMap(mapTexture);

        platfom.Transform.Position = new Vector2(0, 300);
        damageZone.Transform.Position = new Vector2(200, 260);

        Add(worldMap);
        Add(damageZone);
        Add(Player);
        Add(platfom);
    }
}