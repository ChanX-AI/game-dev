using System;
using Platformer2D.Physics;

namespace Platformer2D.Components;

public class DamageZoneComponent : TriggerReceiver {
    public int Damage { get; }

    public DamageZoneComponent(int damage) {
        Damage = damage;
    }

    public override void OnTriggerEntered(CollisionContact contact) {
        var otherCollider = contact.A.Entity == Entity ? contact.B : contact.A;
        var otherEntity = otherCollider.Entity;

        var health = otherEntity?.GetComponent<Health>();
        health?.TakeDamage(Damage);

        Console.WriteLine($"Player Health: {health?.CurrentHealth}");
    }
}