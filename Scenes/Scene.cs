using System;
using System.Collections.Generic;
using Platformer2D.Core;

namespace Platformer2D.Scenes;

public class Scene {
    
    private readonly List<Entity> _entities;
    public IReadOnlyList<Entity> Entities => _entities;
    public event Action<Entity>? EntityAdded;
    public event Action<Entity>? EntityRemoved;

    public Scene() {
        _entities = [];
    }

    public void Add(Entity entity) {
        _entities.Add(entity);
        EntityAdded?.Invoke(entity);
    }

    public void Remove(Entity entity) {
        _entities.Remove(entity);
        EntityRemoved?.Invoke(entity);
    }

}