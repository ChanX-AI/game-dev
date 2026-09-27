using Microsoft.Xna.Framework;
using Platformer2D.Entities;

namespace Platformer2D.Scenes;

public class Level1 : Scene {

    public void Load() {
        var player = new Player();
        var platfom = new Platform();
        var damageZone = new DamageZone();

        platfom.Transform.Position = new Vector2(0, 400);
        damageZone.Transform.Position = new Vector2(200, 360);

        Add(damageZone);
        Add(player);
        Add(platfom);

    }
}