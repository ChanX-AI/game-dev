using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer2D.Controllers;
using Platformer2D.Core;
using Platformer2D.Input;
using Platformer2D.Physics;
using Platformer2D.Rendering;
using Platformer2D.Scenes;

namespace Platformer2D.Game;

public sealed class GameWorld {
    private Renderer _renderer;
    private Scene _scene;
    private PhysicsSystem _physicsSystem;
    private RenderSystem _renderSystem;
    private InputSystem _inputSystem;
    private ControllerSystem _controllerSystem;

    public GameWorld(Scene scene) {
        _scene = scene;
        _inputSystem = new InputSystem();
        _controllerSystem = new ControllerSystem();
        _physicsSystem = new PhysicsSystem();
        _scene.EntityAdded += OnEntityAdded;
        _scene.EntityRemoved += OnEntityRemoved;
        _renderer = null!;
        _renderSystem = null!;
    }

    public void Initialize() {
    }

    public void Load(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
        _renderer = new Renderer(graphicsDevice, spriteBatch);
        _renderer.Load();
        _renderSystem = new RenderSystem(_renderer);
    }

    public void Update(GameTime gameTime) {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _inputSystem.Update();
    }

    public void Draw() {
        _renderSystem.Draw();
    }

    private void OnEntityAdded(Entity entity) {
        entity.ComponentAdded += OnComponentAdded;
        entity.ComponentRemoved += OnComponentRemoved;

        var body = entity.GetComponent<PhysicsBody>();
        if (body is not null) _physicsSystem.Register(body);

        var sprite = entity.GetComponent<SpriteRenderer>();
        if (sprite is not null) _renderSystem.Register(sprite);

        var collider = entity.GetComponent<BoxCollider>();
        if (collider is not null) _physicsSystem.Register(collider);

        var controller = entity.GetComponent<InputController>();
        if (controller is not null) _controllerSystem.Register(controller);
    }

    private void OnEntityRemoved(Entity entity) {
        entity.ComponentAdded -= OnComponentAdded;
        entity.ComponentRemoved -= OnComponentRemoved;

        var body = entity.GetComponent<PhysicsBody>();
        if (body is not null) _physicsSystem.UnRegister(body);

        var sprite = entity.GetComponent<SpriteRenderer>();
        if (sprite is not null) _renderSystem.UnRegister(sprite);

        var collider = entity.GetComponent<BoxCollider>();
        if (collider is not null) _physicsSystem.UnRegister(collider);

        var controller = entity.GetComponent<InputController>();
        if (controller is not null) _controllerSystem.UnRegister(controller);
    }

    private void OnComponentAdded(Component component) {
        if (component is PhysicsBody body) _physicsSystem.Register(body);
        if (component is SpriteRenderer renderer) _renderSystem.Register(renderer);
        if (component is BoxCollider collider) _physicsSystem.Register(collider);
        if (component is InputController controller) _controllerSystem.Register(controller);
    }

    private void OnComponentRemoved(Component component) {
        if (component is PhysicsBody body) _physicsSystem.UnRegister(body);
        if (component is SpriteRenderer renderer) _renderSystem.UnRegister(renderer);
        if (component is BoxCollider collider) _physicsSystem.UnRegister(collider);
        if (component is InputController controller) _controllerSystem.UnRegister(controller);
    }
}