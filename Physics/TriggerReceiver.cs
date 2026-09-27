using Platformer2D.Core;

namespace Platformer2D.Physics;

public class TriggerReceiver : Component {
    public virtual void OnTriggerEntered(CollisionContact contact) {}
    public virtual void OnTriggerStayed(CollisionContact contact) {}
    public virtual void OnTriggerExited(CollisionContact contact) {}
}