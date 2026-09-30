using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;
using Platformer2D.Scenes;

namespace Platformer2D.Rendering;

public sealed class Renderer {
    private readonly SpriteBatch _spriteBatch;
    private readonly GraphicsDevice _graphicsDevice;
    private Texture2D _pixel;
    public Viewport Viewport;

    public Renderer(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
        _graphicsDevice = graphicsDevice;
        _pixel = null!;
        _spriteBatch = spriteBatch;
        Viewport = _graphicsDevice.Viewport;
    }

    public void Load() {
        _pixel = new (_graphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
    }

    public void Begin(Matrix matrix) {
        _spriteBatch.Begin(transformMatrix: matrix);
    }

    public void DrawRect(Vector2 position, Vector2 size, Color color) {
        _spriteBatch.Draw(
            _pixel,
            new Rectangle(
                (int) position.X,
                (int) position.Y,
                (int) size.X,
                (int) size.Y
            ),
            color
        );
    }

    public void DrawTexture(Texture2D texture, Vector2 position, Vector2 size, Color color) {
        _spriteBatch.Draw(
            texture,
            new Rectangle(
                (int) position.X,
                (int) position.Y,
                (int) size.X,
                (int) size.Y
            ),
            color
        );
    }

    public void End() {
        _spriteBatch.End();
    }
}