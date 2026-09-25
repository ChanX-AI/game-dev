using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Platformer2D.Physics;

public class PhysicsSystem {
    
    private readonly HashSet<PhysicsBody> _bodies;
    private readonly HashSet<BoxCollider> _colliders;
    private readonly Vector2 _gravity;

    public PhysicsSystem() {
        _bodies = [];
        _colliders = [];
        _gravity = new(0f, 250f);
    }

    public void Register(PhysicsBody physicsBody) {
        _bodies.Add(physicsBody);
    }

    public void UnRegister(PhysicsBody physicsBody) {
        _bodies.Remove(physicsBody);
    }

    public void Register(BoxCollider collider) {
        _colliders.Add(collider);
    }

    public void UnRegister(BoxCollider collider) {
        _colliders.Remove(collider);
    }

    public void Update(float deltaTime) {
        foreach (var body in _bodies) {
            if (body.Entity == null) continue;
            if (body.UseGravity) body.Accelaration = _gravity;

            body.Velocity += body.Accelaration * deltaTime;
            body.Entity.Transform.Position += body.Velocity * deltaTime;

            DetectCollisions();
        }
    }

    private void DetectCollisions() {
        var colliders = _colliders.ToArray();

        for (int i = 0; i < colliders.Length; i++) {
            for (int j = i + 1; j < colliders.Length; j++) {
                var a = colliders[i];
                var b = colliders[j];

                if (!a.Bounds.Intersects(b.Bounds)) continue;

                var bodyA = a.Entity!.GetComponent<PhysicsBody>();
                var bodyB = b.Entity!.GetComponent<PhysicsBody>();

                if(bodyA != null && bodyB == null) ResolveCollision(a, b);
                if(bodyA == null && bodyB != null) ResolveCollision(b, a);
            }
        }
    }

    private void ResolveCollision(BoxCollider moving, BoxCollider platform) {
       var staticBounds = platform.Bounds;
       var movingBounds = moving.Bounds;
       var body = moving.Entity!.GetComponent<PhysicsBody>();
       if (body == null) return;

       float overlapX = MathF.Min(movingBounds.Right, staticBounds.Right) - MathF.Max(staticBounds.Left, movingBounds.Left);
       float overlapY = MathF.Min(movingBounds.Bottom, staticBounds.Bottom) - MathF.Max(movingBounds.Top, staticBounds.Top);
       
       var position = moving.Entity!.Transform.Position;

       if (overlapX < overlapY) {
            if (movingBounds.Center.X < staticBounds.Center.X) position.X -= overlapX;
            else position.X += overlapX;

            body.Velocity = new Vector2(0, body.Velocity.Y);
        }
        else {
            if (movingBounds.Center.Y < staticBounds.Center.Y) {
                position.Y -= overlapY;
                body.IsGrounded = true;
            }
            else position.Y += overlapY;

            body.Velocity = new Vector2(body.Velocity.X, 0);
        }

        moving.Entity.Transform.Position = position;
    }
}
