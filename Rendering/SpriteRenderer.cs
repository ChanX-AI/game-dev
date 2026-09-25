using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;

namespace Platformer2D.Rendering;

public class SpriteRenderer : Component {
    public Texture2D Texture { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; }

    public SpriteRenderer() {
        Texture = null!;
        Size = new(50, 50);
        Color = Color.Black;
    }
}