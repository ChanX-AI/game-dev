using Microsoft.Xna.Framework.Input;

namespace Platformer2D.Input;

public class InputSystem {
    private KeyboardState _currentKeyboard;
    private KeyboardState _privateKeyboard;

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
}