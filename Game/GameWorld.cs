using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer2D.Core;
using Platformer2D.Input;
using Platformer2D.Physics;
using Platformer2D.Rendering;
using Platformer2D.Scenes;

namespace Platformer2D.Game;

public sealed class GameWorld {
    private Renderer _renderer;
    private Scene _scene;
    private PhysicsSystem _physics;
    private RenderSystem _render;
    private InputSystem _input;

    public GameWorld(Scene scene) {
        _scene = scene;
        _physics = new PhysicsSystem();
        _input = new InputSystem();
        _scene.EntityAdded += OnEntityAdded;
        _scene.EntityRemoved += OnEntityRemoved;
        _renderer = null!;
        _render = null!;
    }

    public void Initialize() {
    }

    public void Load(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
        _renderer = new Renderer(graphicsDevice, spriteBatch);
        _renderer.Load();
        _render = new RenderSystem(_renderer);
    }

    public void Update(GameTime gameTime) {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _input.Update();
        if (_input.IsKeyPressed(Keys.Space)) Console.WriteLine("SPACE");
        _physics.Update(deltaTime);
    }

    public void Draw() {
        _render.Draw();
    }

    private void OnEntityAdded(Entity entity) {
        entity.ComponentAdded += OnComponentAdded;
        entity.ComponentRemoved += OnComponentRemoved;

        var body = entity.GetComponent<PhysicsBody>();
        if (body != null) _physics.Register(body);

        var sprite = entity.GetComponent<SpriteRenderer>();
        if (sprite != null) _render.Register(sprite);

        var collider = entity.GetComponent<BoxCollider>();
        if (collider != null) _physics.Register(collider);
    }

    private void OnEntityRemoved(Entity entity) {
        entity.ComponentAdded -= OnComponentAdded;
        entity.ComponentRemoved -= OnComponentRemoved;

        var body = entity.GetComponent<PhysicsBody>();
        if (body != null) _physics.UnRegister(body);

        var sprite = entity.GetComponent<SpriteRenderer>();
        if (sprite != null) _render.UnRegister(sprite);

        var collider = entity.GetComponent<BoxCollider>();
        if (collider != null) _physics.UnRegister(collider);
    }

    private void OnComponentAdded(Component component) {
        if (component is PhysicsBody body) _physics.Register(body);
        if (component is SpriteRenderer renderer) _render.Register(renderer);
        if (component is BoxCollider collider) _physics.Register(collider);
    }

    private void OnComponentRemoved(Component component) {
        if (component is PhysicsBody body) _physics.UnRegister(body);
        if (component is SpriteRenderer renderer) _render.UnRegister(renderer);
        if (component is BoxCollider collider) _physics.UnRegister(collider);
    }
}