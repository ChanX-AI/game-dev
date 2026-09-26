using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Platformer2D.Input;

public class InputSystem {
    private KeyboardState _currentKeyboard;
    private KeyboardState _privateKeyboard;
    private Dictionary<InputAction, Keys[]> _bindings;

    public InputSystem() {
        _bindings = new() {
            [InputAction.MoveLeft] = [Keys.A, Keys.Left],
            [InputAction.MoveRight] = [Keys.D, Keys.Right],
            [InputAction.Jump] = [Keys.Space]
        };
    }

    public void Update() {
        _privateKeyboard = _currentKeyboard;
        _currentKeyboard = Keyboard.GetState();
    }

    public bool IsKeyDown(Keys key) {
        return _currentKeyboard.IsKeyDown(key);
    }

    public bool IsKeyUp(Keys key) {
        return _currentKeyboard.IsKeyUp(key);
    }

    public bool IsKeyPressed(Keys key) {
        return _currentKeyboard.IsKeyDown(key) && _privateKeyboard.IsKeyUp(key);
    }

    public bool IsKeyReleased(Keys key) {
        return _currentKeyboard.IsKeyUp(key) && _privateKeyboard.IsKeyDown(key);
    }

    public bool IsActionDown(InputAction action) {
        foreach (var key in _bindings[action]) {
           if (IsKeyDown(key)) return true; 
        }
        return false;
    }

    public bool IsActionUp(InputAction action) {
        foreach (var key in _bindings[action]) {
           if (IsKeyUp(key)) return true; 
        }
        return false;
    }

    public bool IsActionPressed(InputAction action) {
        foreach (var key in _bindings[action]) {
           if (IsKeyPressed(key)) return true; 
        }
        return false;
    }

    public bool IsActionReleased(InputAction action) {
        foreach (var key in _bindings[action]) {
           if (IsKeyReleased(key)) return true; 
        }
        return false;
    }
}