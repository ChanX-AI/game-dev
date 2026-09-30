using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;

namespace Platformer2D.Rendering;

public class Camera2D {
    public Vector2 Position;
    public Entity? Target { get; set; }
    public float Zoom { get; set; } = 1f;
    public float FollowSpeed { get; set; } = 5f;

    public void Follow(Entity? target) {
        Target = target;
    }

    public void Update(float deltaTime) {
        if (Target is null) return;

        var sprite = Target.GetComponent<SpriteRenderer>();
        if (sprite is null) return;

        var targetPosition = Target.Transform.Position + sprite.Size * 0.5f;
        float amount = MathF.Min(FollowSpeed * deltaTime, 1f);

        Position += (targetPosition - Position) * amount;
    }

    public Vector2 WorldToScreen(Vector2 worldPosition) {
        return worldPosition - Position;
    }
}