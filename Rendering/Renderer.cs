using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2D.Core;
using Platformer2D.Scenes;

namespace Platformer2D.Rendering;

public sealed class Renderer {
    private readonly SpriteBatch _spriteBatch;
    private readonly GraphicsDevice _graphicsDevice;
    private Texture2D _pixel;

    public Renderer(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
        _graphicsDevice = graphicsDevice;
        _pixel = null!;
        _spriteBatch = spriteBatch;
    }

    public void Load() {
        _pixel = new (_graphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
    }

    // public void Draw(Scene scene) {
    //     foreach (var entity in scene.Entities) {

    //         if (entity is Player player) {
    //             Rectangle rectangle = new (
    //                 (int) player.Transform.Position.X,
    //                 (int) player.Transform.Position.Y,
    //                 (int) player.Size.X,
    //                 (int) player.Size.Y
    //             );

    //             _spriteBatch.Draw(_pixel, rectangle, Color.Black);
    //         }
    //     }
    // }

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
}