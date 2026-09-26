using System;
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

    public void Update(InputSystem input, float deltaTime) {
        foreach (var controller in _controllers) {
            UpdateController(controller, input, deltaTime);
        }
    }

    private void UpdateController(InputController controller, InputSystem input, float deltaTime) {
        var entity = controller.Entity;
        if (entity is null) return;

        var body = entity.GetComponent<PhysicsBody>();
        if (body is null) return;

        float targetVelocity = 0f;

        // Horizontal input movement
        if (input.IsActionDown(InputAction.MoveLeft)) {
            targetVelocity = -controller.MoveSpeed;
        }
        else if (input.IsActionDown(InputAction.MoveRight)) {
            targetVelocity = controller.MoveSpeed;
        }

        // Jump movement
        if (input.IsActionPressed(InputAction.Jump) && body.IsGrounded) {
            body.Velocity = new Vector2(body.Velocity.X, -controller.JumpForce);
        }

        if (input.IsActionReleased(InputAction.Jump) && body.Velocity.Y <= 0f) {
            body.Velocity = new Vector2(body.Velocity.X, -0.5f * body.Velocity.Y);
        }

        // Horizontal Acceleration/Deceleration
        float rate = targetVelocity == 0f ? controller.Deceleration : controller.Acceleration;
        float horizontalVelocity = MoveTowards(body.Velocity.X, targetVelocity, rate * deltaTime);

        body.Velocity = new Vector2(horizontalVelocity, body.Velocity.Y);

    }

    private float MoveTowards(float current, float target, float rate) {
        if (MathF.Abs(target - current) <= rate) return target;

        return current + MathF.Sign(target - current) * rate;
    }
}