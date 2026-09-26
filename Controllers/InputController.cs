using Platformer2D.Core;

namespace Platformer2D.Controllers;

public class InputController : Component {
    public float MoveSpeed { get; set; }
    public float JumpForce { get; set; }
    public float Acceleration { get; set; }
    public float Deceleration { get; set; }
    public float CoyoteTime { get; set; }
    public float JumpBufferTimer { get; set; }

    public InputController() {
        MoveSpeed = 200f;
        JumpForce = 400f;
        Acceleration = 1000f;
        Deceleration = 1500f;
        CoyoteTime = 0.2f;
        JumpBufferTimer = 0.2f;
    }
}