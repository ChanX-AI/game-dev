using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Platformer2D.Input;
using Platformer2D.Physics;

namespace Platformer2D.Controllers;

public sealed class ControllerSystem {
    private HashSet<InputController> _controllers;

    public ControllerSystem() {
        _controllers = [];
    }

    public void Register(InputController controller) {
        _controllers.Add(controller);
    }

    public void UnRegister(InputController controller) {
        _controllers.Remove(controller);
    }

    public void Update(InputSystem input) {
        foreach (var controller in _controllers) {
            UpdateController(controller, input);
        }
    }

    private void UpdateController(InputController controller, InputSystem input) {
        var entity = controller.Entity;
        if (entity is null) return;

        var body = entity.GetComponent<PhysicsBody>();
        if (body is null) return;

        float horizontalVelocity = 0f;

        if (input.IsActionDown(InputAction.MoveLeft)) horizontalVelocity = -200f;
        else if (input.IsActionDown(InputAction.MoveRight)) horizontalVelocity = 200f;

        body.Velocity = new Vector2(horizontalVelocity, body.Velocity.Y);
    }
}