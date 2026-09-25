using Microsoft.Xna.Framework;

namespace Platformer2D.Physics;

public class CollisionContact {
    public BoxCollider A { get; }
    public BoxCollider B { get; }

    public Vector2 Normal { get; }
    public float Penetration { get; }

    public CollisionContact(BoxCollider a, BoxCollider b, Vector2 normal, float penetration) {
        A = a;
        B = b;
        Normal = normal;
        Penetration = penetration;
    }
}