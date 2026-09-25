using Microsoft.Xna.Framework;

namespace Platformer2D.Math;

public sealed class Transform {
    
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; }

    public Transform() {
        Position = Vector2.Zero;
        Rotation = 0f;
        Scale = Vector2.One;
    }
}