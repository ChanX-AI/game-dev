using System.Collections.Generic;

namespace Platformer2D.Rendering;

public sealed class RenderSystem {
    private readonly List<SpriteRenderer> _sprites;
    private Renderer _renderer;

    public RenderSystem(Renderer renderer) {
        _sprites = [];
        _renderer = renderer;
    }

    public void Register(SpriteRenderer renderer) {
        _sprites.Add(renderer);
    }

    public void UnRegister(SpriteRenderer renderer) {
        _sprites.Remove(renderer);
    }

    public void Draw() {
        foreach (var sprite in _sprites) {
            if (sprite.Entity == null) continue;
            var position = sprite.Entity.Transform.Position;
            _renderer.DrawRect(position, sprite.Size, sprite.Color);
        }   
    }
}