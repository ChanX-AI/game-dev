using Platformer2D.Core;

namespace Platformer2D.Controllers;

public class InputController : Component {
    public float MoveSpeed { get; set; }
    public float JumpForce { get; set; }

    public InputController() {
        MoveSpeed = 200f;
        JumpForce = 400f;
    }
}