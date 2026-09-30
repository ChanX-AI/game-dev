using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer2D.Rendering;

public sealed class RenderSystem {
    private readonly List<SpriteRenderer> _sprites;
    private readonly Renderer _renderer;
    private readonly Camera2D _camera;

    public RenderSystem(Renderer renderer, Camera2D camera) {
        _sprites = [];
        _renderer = renderer;
        _camera = camera;
    }

    public void Register(SpriteRenderer renderer) {
        _sprites.Add(renderer);
    }

    public void UnRegister(SpriteRenderer renderer) {
        _sprites.Remove(renderer);
    }

    public void Draw() {
        Matrix cameraMatrix = 
            Matrix.CreateTranslation(
                -_camera.Position.X,
                -_camera.Position.Y,
                0f
            )
            * Matrix.CreateScale(_camera.Zoom)
            * Matrix.CreateTranslation(
                _renderer.Viewport.Width / 2f,
                _renderer.Viewport.Height / 2f,
                0f
            );
        _renderer.Begin(cameraMatrix);
        foreach (var sprite in _sprites) {
            if (sprite.Entity == null) continue;
            //var screenDim = new Vector2(_renderer.Viewport.Width, _renderer.Viewport.Height);
            //var screenPosition = (sprite.Entity.Transform.Position - _camera.Position) * 1.5f + screenDim * 0.5f;
            //var screenWidth = sprite.Size * 1.5f;
            var position = sprite.Entity.Transform.Position;
            if (sprite.Texture == null)
                _renderer.DrawRect(position, sprite.Size, sprite.Color);
            else
                _renderer.DrawTexture(sprite.Texture, position, sprite.Size, sprite.Color);
        }
        _renderer.End();
    }
}