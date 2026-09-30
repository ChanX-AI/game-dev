using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;
using Platformer2D.Physics;
using Platformer2D.Rendering;

namespace Platformer2D.Entities;

public class WorldMap : Entity {
    
    public WorldMap(Texture2D texture) {
        AddComponent(new SpriteRenderer() {
            Texture = texture,
            Size = new Vector2(texture.Width, texture.Height)
        });
    }
}