namespace Platformer2D.Core;

public abstract class Component {
    
    public Entity? Entity { get; internal set; } = null!;
}