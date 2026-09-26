using System.Collections.Generic;

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

    public void Update() {
        foreach (var controller in _controllers) {
            
        }
    }
}