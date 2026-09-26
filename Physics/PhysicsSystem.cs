using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        DetectCollisions();
    }

    private void DetectCollisions() {
        var colliders = _colliders.ToArray();

        for (int i = 0; i < colliders.Length; i++) {
            for (int j = i + 1; j < colliders.Length; j++) {

                var a = colliders[i];
                var b = colliders[j];

                var bodyA = a.Entity!.GetComponent<PhysicsBody>();
                var bodyB = b.Entity!.GetComponent<PhysicsBody>();

                if (bodyA is null && bodyB is null) continue;

                BoxCollider moving;
                BoxCollider other;

                if (bodyA is not null && bodyB is null) {
                    moving = a;
                    other = b;
                }
                else if (bodyA is null && bodyB is not null) {
                    moving = b;
                    other = a;
                }
                else {
                    continue;
                }

                CollisionContact? contact = DetectCollision(moving, other);
                if (contact is null) continue;
                ResolveCollision(contact);
            }
        }
    }

    private CollisionContact? DetectCollision(BoxCollider a, BoxCollider b) {
        var aBounds = a.Bounds;
        var bBounds = b.Bounds;

        if (!aBounds.Intersects(bBounds)) return null;

        float overlapX = MathF.Min(aBounds.Right, bBounds.Right) - MathF.Max(aBounds.Left, bBounds.Left);
        float overlapY = MathF.Min(aBounds.Bottom, bBounds.Bottom) - MathF.Max(aBounds.Top, bBounds.Top);

        Vector2 normal;
        float penetration;

        if (overlapX < overlapY) {
            normal = aBounds.Center.X < bBounds.Center.X ? new Vector2(-1, 0) : new Vector2(1, 0);
            penetration = overlapX;
        }
        else {
            normal = aBounds.Center.Y < bBounds.Center.Y ? new Vector2(0, -1) : new Vector2(0, 1);
            penetration = overlapY;
        }

        return new CollisionContact(a, b, normal, penetration);
    }

    private void ResolveCollision(CollisionContact contact) {
        var body = contact.A.Entity?.GetComponent<PhysicsBody>();
        if (body is null) return;

        var velocity = body.Velocity;

        if (contact.Normal.X != 0) velocity.X = 0;
        if (contact.Normal.Y != 0) {
            velocity.Y = 0;
            if (contact.Normal.Y < 0) body.IsGrounded = true;
        }
        contact.A.Entity!.Transform.Position += contact.Penetration * contact.Normal;
        body.Velocity = velocity;
    }
}
