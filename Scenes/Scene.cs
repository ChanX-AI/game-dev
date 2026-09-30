using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Platformer2D.Core;

namespace Platformer2D.Scenes;

public class Scene {
    
    private readonly List<Entity> _entities;
    public IReadOnlyList<Entity> Entities => _entities;
    public event Action<Entity>? EntityAdded;
    public event Action<Entity>? EntityRemoved;
    public virtual Entity? CameraTarget => null;

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

    public virtual void Load(ContentManager content) {}

}