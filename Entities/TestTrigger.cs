using System;
using Platformer2D.Physics;

namespace Platformer2D.Entities;

public class TestTrigger : TriggerReceiver {
    public override void OnTriggerEntered(CollisionContact contact) {
        Console.WriteLine("PLAYER: Trigger Entered");
    }

    public override void OnTriggerStayed(CollisionContact contact) {
        Console.WriteLine("PLAYER: Trigger Stayed");
    }

    public override void OnTriggerExited(CollisionContact contact) {
        Console.WriteLine("PLAYER: Trigger Exited");
    }
}