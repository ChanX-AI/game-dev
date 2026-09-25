using System;
using Microsoft.Xna.Framework;
using Platformer2D.Core;
using SharpDX.Direct3D9;

namespace Platformer2D.Physics;

public sealed class PhysicsBody : Component {
    
    public Vector2 Velocity { get; set; }
    public Vector2 Accelaration { get; set; }
    public bool UseGravity { get; set; }
    public bool IsGrounded { get; internal set; }

    public PhysicsBody() {
        Velocity = Vector2.Zero;
        Accelaration = Vector2.Zero;
        UseGravity = true;
    }
}