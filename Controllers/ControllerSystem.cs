using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Platformer2D.Input;
using Platformer2D.Physics;

namespace Platformer2D.Controllers;

public sealed class ControllerSystem {
    private HashSet<InputController> _controllers;
    private float _coyoteTimer;
    private float _jumpBufferTimer;

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

        UpdateCoyoteTimer(controller, body, deltaTime);
        UpdateJumpBuffer(input, controller, deltaTime);
        UpdateJump(input, controller, body);
        UpdateHorizontalMovement(input, controller, body, deltaTime);
    }

    private float MoveTowards(float current, float target, float rate) {
        if (MathF.Abs(target - current) <= rate) return target;

        return current + MathF.Sign(target - current) * rate;
    }

    private void UpdateCoyoteTimer(InputController controller, PhysicsBody body, float deltaTime) {
        if (body.IsGrounded) _coyoteTimer = controller.CoyoteTime;
        else _coyoteTimer -= deltaTime;
    }

    private void UpdateJumpBuffer(InputSystem input, InputController controller, float deltaTime) {
        if (input.IsActionPressed(InputAction.Jump)) {
            _jumpBufferTimer = controller.JumpBufferTimer;
        }
        else {
            _jumpBufferTimer -= deltaTime;
        }
    }

    private void UpdateHorizontalMovement(InputSystem input, InputController controller, PhysicsBody body, float deltaTime) {
        float targetVelocity = 0f;
        if (input.IsActionDown(InputAction.MoveLeft)) {
            targetVelocity = -controller.MoveSpeed;
        }
        else if (input.IsActionDown(InputAction.MoveRight)) {
            targetVelocity = controller.MoveSpeed;
        }

        // Horizontal Acceleration/Deceleration
        float rate = targetVelocity == 0f ? controller.Deceleration : controller.Acceleration;
        float horizontalVelocity = MoveTowards(body.Velocity.X, targetVelocity, rate * deltaTime);

        body.Velocity = new Vector2(horizontalVelocity, body.Velocity.Y);
    }

    private void UpdateJump(InputSystem input, InputController controller, PhysicsBody body) {
        if (_jumpBufferTimer > 0f && _coyoteTimer > 0f) {
            body.Velocity = new Vector2(body.Velocity.X, -controller.JumpForce);
            _coyoteTimer = 0;
        }

        if (input.IsActionReleased(InputAction.Jump) && body.Velocity.Y <= 0f) {
            body.Velocity = new Vector2(body.Velocity.X, -0.5f * body.Velocity.Y);
        }
    }
}