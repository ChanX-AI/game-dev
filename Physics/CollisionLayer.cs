using System;

namespace Platformer2D.Physics;

[Flags]
public enum CollisionLayer {
    None    = 0,
    Player  = 1 << 0,
    World   = 1 << 1,
    Enemy   = 1 << 2,
    Trigger = 1 << 3
}