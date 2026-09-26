using System;
using System.Collections.Generic;
using System.Linq;
using Platformer2D.Math;

namespace Platformer2D.Core;

public class Entity {
    
    public Transform Transform { get; }
    private readonly List<Component> _components = [];
    public event Action<Component>? ComponentAdded;
    public event Action<Component>? ComponentRemoved;

    protected Entity() {
        Transform = new();
    }

    public void AddComponent(Component component) {
        component.Entity = this;
        _components.Add(component);
        ComponentAdded?.Invoke(component);
    }

    public bool RemoveComponent<T>() where T : Component {
        var component = GetComponent<T>();
        if (component is null) return false;
        
        _components.Remove(component);
        component.Entity = null;
        ComponentRemoved?.Invoke(component);

        return true;
  
    }

    public T? GetComponent<T>() where T: Component {
        return _components.OfType<T>().FirstOrDefault();
    }
}
