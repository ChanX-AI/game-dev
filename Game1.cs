using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer2D.Game;
using Platformer2D.Scenes;

namespace Platformer2D;

public class Game1 : Microsoft.Xna.Framework.Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameWorld _world;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        _spriteBatch = null!;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _world = null!;
    }

    protected override void Initialize() {
        //_world.Initialize();
        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var scene = new Level1();
        _world = new GameWorld(scene);
        _world.Load(GraphicsDevice, _spriteBatch);
         scene.Load();
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _world.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _world.Draw();
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
