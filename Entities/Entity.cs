using System.Drawing;
using System.Numerics;

public abstract class Entity
{
    public Vector2 Position { get; protected set; }

    public Entity(Vector2 position) {Position = position;}
    public virtual void Update() {}

    public virtual void Draw() {}
}
